using Frontend.Helpers.Serializers;
using Frontend.Model.Blocks;
using Frontend.Models.Blocks.Shapes;
using Frontend.Views;
using System.Text.Json.Serialization;
using System.Text.Json;
using Frontend.Models.Blocks.Descriptors;

namespace Frontend.ViewModels
{
    /// <summary>
    /// Classe che rappresenta un ViewModel per la <see cref="BlockView"/>
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
        public static List<string> FunctionNames = new();

        /// <summary>
        /// Lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene tutti i blocchi
        /// </summary>
        private List<IFrontEndBlock> _allBlocks;

        /// <summary>
        /// Lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene effettivamente i blocchi mostrati all'utente, 
        /// che possono essere trascinati
        /// </summary>
        private List<IFrontEndBlock> _blocks;

        /// <summary>
        /// Lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene tutti i blocchi
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
        /// Lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene effettivamente i blocchi trascinati dall'utente
        /// </summary>
        private List<IFrontEndBlock> _droppedBlocks;

        /// <summary>
        /// Lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene i blocchi trascinati dall'utente
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
        /// <param name="droppedBlock"> Blocco selezionato </param>
        /// <param name="dropPoint"> Punto di rilascio del blocco </param>
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
        /// <param name="droppedBlock"> Blocco <see cref="IFrontEndBlock"/> trascinato da aggiungere alla lista </param>
        public void AddDroppedBlock(IFrontEndBlock droppedBlock, PointF dropPoint)
        {
            var underBlock = DroppedBlocks.Where(block => Contains(block, dropPoint)).LastOrDefault();
            
            if (droppedBlock.Descriptor.Type == BlockType.DefinizioneFunzione) FunctionNames.Add(droppedBlock.Questions.ElementAt(0).Value);
            ShiftBlocksWhenDropped(droppedBlock, SetUpperLeft(new(dropPoint.X, dropPoint.Y), droppedBlock, underBlock));
            DroppedBlocks = DroppedBlocks.Append(droppedBlock).ToList();
        }

        /// <summary>
        /// Imposta le coordinate del punto in alto a sinistra del blocco trascinato, in base al punto di rilascio e al blocco 
        /// che contiene quest'ultimo
        /// </summary>
        /// <param name="originalPosition"> Posizione di rilascio del blocco </param>
        /// <param name="dropped"> Blocco trascinato </param>
        /// <param name="under"> Eventuale blocco contenente il punto di rilascio </param>
        /// <returns> Il blocco dal quale shiftare </returns>
        private IFrontEndBlock SetUpperLeft(PointF originalPosition, IFrontEndBlock dropped, IFrontEndBlock under)
        {
            IFrontEndBlock returnBlock = null;

            if (dropped.Descriptor.Type != BlockType.Principale && dropped.Descriptor.Type != BlockType.DefinizioneFunzione)
            {
                PointF upperCorner = new(under.Position.UpperLeft.X, under.Position.UpperLeft.Y + 40);
                PointF bottomCorner = new(under.Position.BottomRight.X, under.Position.BottomRight.Y - 40);
                if (under.CanContainChildren && Contains(upperCorner, bottomCorner, originalPosition))
                {
                    var lastChildren = under.Children.LastOrDefault();
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

                    var current = under;
                    while(current?.Shape.Type == ShapeType.WITH_CHILDREN)
                    {
                        current.Height += dropped.Shape.BlockOffset.Y;
                        if (current.Father.Shape.Type != ShapeType.WITH_CHILDREN)
                            break;
                        current = current.Father;
                    }

                    under.Shape.BlockOffset = new(under.Shape.BlockOffset.X, under.Shape.BlockOffset.Y + dropped.Shape.BlockOffset.Y);
                    under.Children.Add(dropped);
                    dropped.Father = under = current;
                    returnBlock = under.Next;
                }
                else
                {
                    originalPosition.X = under.Position.UpperLeft.X;
                    originalPosition.Y = under.Position.UpperLeft.Y + under.Shape.BlockOffset.Y;
                    dropped.Father = under.Father ?? under;
                    dropped.Next = under.Next;
                    under.Next = dropped;
                    returnBlock = dropped.Next;
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
            return returnBlock;
        }

        /// <summary>
        /// Elimina il blocco passato come parametro
        /// </summary>
        /// <param name="deletedBlock"> Blocco da eliminare </param>
        public void DeleteDroppedBlock(IFrontEndBlock deletedBlock)
        {
            deletedBlock.Children.ForEach(child => { DroppedBlocks.Remove(child); });

            if (deletedBlock.Father != null) deletedBlock.Father.Next = deletedBlock.Next;
            if (deletedBlock.Father?.Shape.Type == ShapeType.WITH_CHILDREN) deletedBlock.Father.Height -= deletedBlock.Shape.BlockOffset.Y;
            
            ShiftBlocksWhenDelete(deletedBlock);
            deletedBlock.Father?.Children.Remove(deletedBlock);
            DroppedBlocks = DroppedBlocks.Where(x => !x.Equals(deletedBlock)).ToList();
        }

        /// <summary>
        /// Sposta i blocchi quando un nuovo blocco viene rilasciato
        /// </summary>
        /// <param name="droppedBlock"> Blocco rilasciato </param>
        /// <param name="underBlock"> Blocco contenente il punto di rilascio </param>
        private void ShiftBlocksWhenDropped(IFrontEndBlock droppedBlock, IFrontEndBlock underBlock)
        {
            if (droppedBlock.Descriptor.Type != BlockType.Principale && droppedBlock.Descriptor.Type != BlockType.DefinizioneFunzione)
                Shift(underBlock, droppedBlock.Shape.BlockOffset.Y, GetCurrent, (x, y) => x + y);
        }
        /// <summary>
        /// Sposta i blocchi quando un blocco, precedentemente rilasciato, viene eliminato
        /// </summary>
        /// <param name="deletedBlock"> Blocco eliminato </param>
        private void ShiftBlocksWhenDelete(IFrontEndBlock deletedBlock)
        {
            Shift(deletedBlock, deletedBlock.Shape.BlockOffset.Y, GetCurrent, (x, y) => x - y);
        }
        /// <summary>
        /// Sposta i blocchi, a partire da quello posizionato sotto al blocco di partenza passato come parametro
        /// </summary>
        /// <param name="currentBlock"> Blocco di partenza </param>
        /// <param name="offset"> Offset che indica di quanto i blocchi vadano spostati </param>
        /// <param name="findNextFunction"> Funzione per trovare il prossimo blocco da spostare </param>
        /// <param name="YSetterFunction"> Funzione per impostare le coordinate del punto in alto a sinistra del blocco da spostare </param>
        private void Shift(IFrontEndBlock currentBlock, float offset, Func<IFrontEndBlock, IFrontEndBlock, bool> findNextFunction, Func<float, float, float> YSetterFunction)
        {
            var current = currentBlock;
            IFrontEndBlock next = current;//DroppedBlocks.Find(b => findNextFunction.Invoke(b, current));

            while (next != null)
            {
                foreach (var child in next.Children)
                    child.Position.UpperLeft = new(child.Position.UpperLeft.X, YSetterFunction.Invoke(child.Position.UpperLeft.Y, offset));

                current = next;
                next = current.Next;//DroppedBlocks.Find(b => findNextFunction.Invoke(b, current));
                current.Position.UpperLeft = new(current.Position.UpperLeft.X, YSetterFunction.Invoke(current.Position.UpperLeft.Y, offset));
            }
        }

        /// <summary>
        /// Verifica se un blocco è posizionato sotto ad un altro
        /// </summary>
        /// <param name="block"> Blocco del quale verificare la posizione </param>
        /// <param name="current"> Blocco corrente </param>
        /// <returns> se esiste un blocco è posizionato sotto ad un altro, false altrimenti </returns>
        private bool GetCurrent(IFrontEndBlock block, IFrontEndBlock current)
        {
            return block.Position.UpperLeft.X == current.Position.UpperLeft.X && block.Position.UpperLeft.Y == current.Position.UpperLeft.Y + current.Shape.BlockOffset.Y;
        }
        /// <summary>
        /// Restitusce un blocco contenente il punto passato come parametro, o null se questo non esiste
        /// </summary>
        /// <param name="pointF"> Punto dal quale estarre un blocco </param>
        /// <returns> un blocco contenente il punto passato come parametro, o null se questo non esiste </returns>
        public IFrontEndBlock GetBlockFromPoint(PointF pointF)
        {
            return DroppedBlocks.Where(block => Contains(block, pointF)).LastOrDefault();
        }

        /// <summary>
        /// Verifica se un blocco contiene il punto passato come parametro
        /// </summary>
        /// <param name="block"> Blocco con il quale effettuare la verifica </param>
        /// <param name="point"> Punto del quale verificare l'appartenenza al blocco </param>
        /// <returns> se il blocco contiene il punto passato come parametro, false altrimenti </returns>
        public static bool Contains(IFrontEndBlock block, PointF point)
        {
            return Contains(block.Position.UpperLeft, block.Position.BottomRight, point);
        }
        /// <summary>
        /// Verifica se un punto, passato come parametro, è compreso tra altri 2
        /// </summary>
        /// <param name="upperCorner"> Primo punto </param>
        /// <param name="bottomCorner"> Secondo punto </param>
        /// <param name="point"> Punto su cui viene effettuata la verifica </param>
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
        /// <param name="type"> Tipo di blocco in base al quale filtrare la lista </param>
        public void UpdateBlocksByType(BlockType type)
        {
            Blocks = _allBlocks.FindAll((e) => e.Descriptor.Type.Equals(type));
        }

        /// <summary>
        /// Restituisce una stringa, in formato Json, che rappresenta i blocchi posizionati
        /// </summary>
        /// <returns> una stringa in formato Json </returns>
        public string GetJsonDroppedBlocks()
        {
            List<FEBlockSerializable> list = new();

            foreach (var item in DroppedBlocks)
            {
                list.Add(new FEBlockSerializable(item));
            }

            return JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                WriteIndented = true
            });
        }

        /// <summary>
        /// Imposta la lista dei blocchi posizionati a partire da una stringa formato Json passata come parametro
        /// </summary>
        /// <param name="droppedBlocks"> Stringa, in formato Json, che rappresenta i blocchi posizionati </param>
        public void SetDroppedBlocksFromJson(string droppedBlocks)
        {
            List<FEBlockSerializable> serializedBlocks = JsonSerializer.Deserialize<List<FEBlockSerializable>>(droppedBlocks);
            List<IFrontEndBlock> blocks = new();
            foreach (var block in serializedBlocks)
            {
                IFrontEndBlock baseBlock = (IFrontEndBlock)Activator.CreateInstance(Type.GetType(block.BlockType));
                IFrontEndBlock serialize = baseBlock.GetInfo();
                serialize.Position = block.Position;
                serialize.Descriptor = new BlockDescriptor(block.DescriptorName, block.DescriptorType);

                foreach (var question in block.Questions)
                    serialize.Questions.ElementAt(question.Item1).SetValue(question.Item2);
                
                blocks.Add(serialize);
            }

            DroppedBlocks = blocks;
        }
    }
}
