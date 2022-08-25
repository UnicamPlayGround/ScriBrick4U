using Frontend.Model.Blocks;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per i blocchi
    /// </summary>
    public class BlockViewModel : BaseViewModel
    {
        /// <summary>
        /// Canvas, di tipo <see cref="GraphicsView"/>, presente nella BlockView
        /// </summary>
        private readonly GraphicsView _graphicsView;

        /// <summary>
        /// Lista contenente i nomi dei blocchi che chiamano funzioni
        /// </summary>
        private readonly List<string> _functionNames;

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene tutti i blocchi
        /// </summary>
        private List<IFrontEndBlock> _allBlocks;

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene effettivamente i blocchi mostrati all'utente, 
        /// che possono essere trascinati
        /// </summary>
        private List<IFrontEndBlock> _blocks;

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene tutti i blocchi
        /// </summary>
        public List<IFrontEndBlock> Blocks
        {
            get => _blocks;
            set
            {
                _blocks = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene effettivamente i blocchi trascinati dall'utente
        /// </summary>
        private List<IFrontEndBlock> _droppedBlocks;

        /// <summary>
        /// lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene i blocchi trascinati dall'utente
        /// </summary>
        public List<IFrontEndBlock> DroppedBlocks
        {
            get => _droppedBlocks;
            set
            {
                _droppedBlocks = value;
                _graphicsView.Invalidate();
            }
        }

        /// <summary>
        /// Costruttore di default
        /// </summary>
        /// <param name="droppedBlocksGraphicsView"> Canvas, di tipo <see cref="GraphicsView"/>, presente nella BlockView </param>
        public BlockViewModel(GraphicsView droppedBlocksGraphicsView)
        {
            SetMediator(this);
            InitBlocksList();
            _graphicsView = droppedBlocksGraphicsView;
            _functionNames = new();
            DroppedBlocks = new();
        }

        /// <summary>
        /// Metodo che inizializza la lista contenente tutti i blocchi <see cref="IFrontEndBlock"/>
        /// </summary>
        private void InitBlocksList()
        {
            _allBlocks = new();
            foreach (var block in AbstractFrontEndBlock.GetEnumerableOfType())
            {
                _allBlocks.Add(block.GetInfo());
            }

            Blocks = _allBlocks;
        }

        /// <summary>
        /// Verifica se un blocco può essere rilasciato nel punto passato come parametro
        /// </summary>
        /// <param name="droppedBlock"> blocco selezionato </param>
        /// <param name="dropPoint"> punto di rilascio del blocco </param>
        /// <returns> true se il blocco puo' essere posizionato nel punto indicato </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool CanBeDropped(IFrontEndBlock droppedBlock, PointF dropPoint)
        {
            if (droppedBlock.Descriptor.Type.Equals(BlockType.Principale) && DroppedBlocks.Exists(block => droppedBlock.Descriptor.Name.Equals(block.Descriptor.Name)))
                throw new InvalidOperationException("Un blocco '" + droppedBlock.Descriptor.Name.ToUpper() + "' è già stato posizionato.");
            if (GetBlockFromPoint(dropPoint) == null && !droppedBlock.Descriptor.Type.Equals(BlockType.DefinizioneFunzione) && !droppedBlock.Descriptor.Type.Equals(BlockType.Principale))
                throw new InvalidOperationException("Il blocco '" + droppedBlock.Descriptor.Name.ToUpper() + "' puo' essere posizionato solamente sotto ad una altro blocco");

            return true;
        }

        /// <summary>
        /// Metodo che permette di aggiungere, alla lista dei blocchi trascinati, un nuovo blocco <see cref="IFrontEndBlock"/>
        /// </summary>
        /// <param name="droppedBlock"> blocco <see cref="IFrontEndBlock"/> trascinato da aggiungere alla lista </param>
        /// <exception cref="NullReferenceException"> se il blocco passato è nullo </exception>
        public void AddDroppedBlock(IFrontEndBlock droppedBlock, PointF dropPoint)
        {
            var underBlock = DroppedBlocks.Where(block => Contains(block, dropPoint)).FirstOrDefault();
            SetUpperLeft(new(dropPoint.X, dropPoint.Y), droppedBlock, underBlock);

            if (droppedBlock.Descriptor.Type == BlockType.DefinizioneFunzione) _functionNames.Add(droppedBlock.Descriptor.Name);
            if (droppedBlock.Descriptor.Type != BlockType.Principale && droppedBlock.Descriptor.Type != BlockType.DefinizioneFunzione)
                ShiftBlocksWhenDropped(droppedBlock, underBlock);
            DroppedBlocks = DroppedBlocks.Append(droppedBlock).ToList();
        }

        /// <summary>
        /// Imposta le coordinate del punto in alto a sinistra del blocco trascinato, in base al punto di rilascio e al blocco 
        /// che contiene quest'ultimo
        /// </summary>
        /// <param name="originalPosition"> posizione di rilascio del blocco </param>
        /// <param name="dropped"> blocco trascinato </param>
        /// <param name="under"> eventuale blocco contenente il punto di rilascio </param>
        private void SetUpperLeft(PointF originalPosition, IFrontEndBlock dropped, IFrontEndBlock under)
        {
            if (!dropped.Descriptor.Type.Equals(BlockType.Principale) && !dropped.Descriptor.Type.Equals(BlockType.DefinizioneFunzione))
            {
                PointF upperCorner = new(under.Position.UpperLeft.X, under.Position.UpperLeft.Y + 40);
                PointF bottomCorner = new(under.Position.BottomRight.X, under.Position.BottomRight.Y - 40);
                if (under.Shape.Type.Equals(ShapeType.WITH_CHILDREN) && Contains(upperCorner, bottomCorner, originalPosition))
                {
                    var lastChildren = DroppedBlocks
                                            .Where(block => !block.Equals(under) && Contains(under, block.Position.UpperLeft))
                                            .LastOrDefault();
                    if (lastChildren == null)
                    {
                        //16 è un numero (giusto) tirato a caso sarebbe da sistemare
                        upperCorner.X += 16;
                        originalPosition = upperCorner;
                    }
                    else
                    {
                        originalPosition.X = lastChildren.Position.UpperLeft.X;
                        originalPosition.Y = lastChildren.Position.UpperLeft.Y + lastChildren.Shape.BlockOffset.Y;
                    }
                    under.Height += dropped.Height;
                    under.Children.Add(dropped);
                    dropped.Father = under;
                }
                else
                {
                    originalPosition.X = under.Position.UpperLeft.X;
                    originalPosition.Y = under.Position.UpperLeft.Y + under.Shape.BlockOffset.Y;
                    dropped.Father = under.Father ?? under;
                    dropped.Father.Children.Add(dropped);
                }
            }
            else
            {
                IFrontEndBlock bl;
                while ((bl = GetBlockFromPoint(originalPosition)) != null)
                    originalPosition.X += (bl.Position.BottomRight.X - originalPosition.X) + 20;
            }
            dropped.Position.UpperLeft = originalPosition;
        }

        /// <summary>
        /// Elimina il blocco passato come parametro
        /// </summary>
        /// <param name="deletedBlock"> blocco da eliminare </param>
        public void DeleteDroppedBlock(IFrontEndBlock deletedBlock)
        {
            deletedBlock.Children.ForEach(child => DroppedBlocks.Remove(child));
            ShiftBlocksWhenDelete(deletedBlock);
            DroppedBlocks = DroppedBlocks.Where(x => !x.Equals(deletedBlock)).ToList();
        }

        /// <summary>
        /// Sposta i blocchi quando un nuovo blocco viene rilasciato
        /// </summary>
        /// <param name="droppedBlock"> blocco rilasciato </param>
        /// <param name="underBlock"> blocco contenente il punto di rilascio </param>
        private void ShiftBlocksWhenDropped(IFrontEndBlock droppedBlock, IFrontEndBlock underBlock)
        {
            if (droppedBlock.Descriptor.Type != BlockType.Principale && droppedBlock.Descriptor.Type != BlockType.DefinizioneFunzione)
                Shift(underBlock, droppedBlock.Shape.BlockOffset.Y, GetCurrent, (x, y) => x + y);
        }
        /// <summary>
        /// Sposta i blocchi quando un blocco, precedentemente rilasciato, viene eliminato
        /// </summary>
        /// <param name="deletedBlock"> blocco eliminato </param>
        private void ShiftBlocksWhenDelete(IFrontEndBlock deletedBlock)
        {
            Shift(deletedBlock, deletedBlock.Shape.BlockOffset.Y, GetCurrent, (x, y) => x - y);
        }
        /// <summary>
        /// Sposta i blocchi, a partire da quello posizionato sotto al blocco di partenza passato come parametro
        /// </summary>
        /// <param name="currentBlock"> blocco di partenza </param>
        /// <param name="offset"> offset che indica di quanto i blocchi vadano spostati </param>
        /// <param name="findNextFunction"> funzione per trovare il prossimo blocco da spostare </param>
        /// <param name="YSetterFunction"> funzione per impostare le coordinate del punto in alto a sinistra del blocco da spostare </param>
        private void Shift(IFrontEndBlock currentBlock, float offset, Func<IFrontEndBlock, IFrontEndBlock, bool> findNextFunction, Func<float, float, float> YSetterFunction)
        {
            var current = currentBlock;
            IFrontEndBlock next = DroppedBlocks.Find(b => findNextFunction.Invoke(b, current));

            while (next != null)
            {
                current = next;
                next = DroppedBlocks.Find(b => findNextFunction.Invoke(b, current));
                current.Position.UpperLeft = new(current.Position.UpperLeft.X, YSetterFunction.Invoke(current.Position.UpperLeft.Y, offset));
            }
        }

        /// <summary>
        /// Verifica se un blocco è posizionato sotto ad un altro
        /// </summary>
        /// <param name="block"> blocco del quale verificare la posizione </param>
        /// <param name="current"> blocco corrente </param>
        /// <returns> se esiste un blocco è posizionato sotto ad un altro, false altrimenti </returns>
        private bool GetCurrent(IFrontEndBlock block, IFrontEndBlock current)
        {
            return block.Position.UpperLeft.X == current.Position.UpperLeft.X && block.Position.UpperLeft.Y == current.Position.UpperLeft.Y + current.Shape.BlockOffset.Y;
        }
        /// <summary>
        /// Restitusce un blocco contenente il punto passato come parametro, o null se questo non esiste
        /// </summary>
        /// <param name="pointF"> punto dal quale estarre un blocco </param>
        /// <returns> un blocco contenente il punto passato come parametro, o null se questo non esiste </returns>
        public IFrontEndBlock GetBlockFromPoint(PointF pointF)
        {
            return DroppedBlocks.Where(block => Contains(block, pointF)).FirstOrDefault();
        }

        /// <summary>
        /// Verifica se un blocco contiene il punto passato come parametro
        /// </summary>
        /// <param name="block"> blocco con il quale effettuare la verifica </param>
        /// <param name="point"> punto del quale verificare l'appartenenza al blocco </param>
        /// <returns> se il blocco contiene il punto passato come parametro, false altrimenti </returns>
        public static bool Contains(IFrontEndBlock block, PointF point)
        {
            return Contains(block.Position.UpperLeft, block.Position.BottomRight, point);
        }
        /// <summary>
        /// Verifica se un punto, passato come parametro, è compreso tra altri 2
        /// </summary>
        /// <param name="upperCorner"> primo punto </param>
        /// <param name="bottomCorner"> secondo punto </param>
        /// <param name="point"> punto su cui viene effettuata la verifica </param>
        /// <returns> true se il punto passato come parametro è compreso tra altri 2, false altrimenti </returns>
        public static bool Contains(PointF upperCorner, PointF bottomCorner, PointF point)
        {
            if (upperCorner.X > point.X || bottomCorner.X < point.X) return false;
            if (upperCorner.Y > point.Y || bottomCorner.Y < point.Y) return false;
            return true;
        }

        /// <summary>
        /// Metodo che aggiorna la lista dei blocchi mostrati all'utente in base al tipo
        /// </summary>
        /// <param name="type"> tipo di blocco in base al quale filtrare la lista </param>
        public void UpdateBlocksByType(BlockType type)
        {
            Blocks = _allBlocks.FindAll((e) => e.Descriptor.Type.Equals(type));
        }

        /// <summary>
        /// Restituisce una stringa, in formato Json, che rappresenta i blocchi trascinati
        /// </summary>
        /// <returns> uan stringa in formato Json </returns>
        public string GetJsonDroppedBlocks()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Imposta la lista dei blocchi trascinati a partire da una stringa formato Json passata come parametro
        /// </summary>
        /// <param name="droppedBlocks"> stringa che rappresenta i blocchi trascinati </param>
        public void SetDroppedBlocksFromJson(string droppedBlocks)
        {
            throw new NotImplementedException();
        }
    }
}
