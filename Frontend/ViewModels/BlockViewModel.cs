using Frontend.Helpers.Serializers;
using Frontend.Views;
using System.Text.Json.Serialization;
using System.Text.Json;
using Frontend.Models.Blocks;
using Frontend.Models.Blocks.Shapes;
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
        /// Lista contenente i nomi delle funzioni definite
        /// </summary>
        public static List<string> FunctionNames { get; set; } = new();
        /// <summary>
        /// Lista contenente i nomi delle variabili definite
        /// </summary>
        public static List<string> VariableNames { get; set; } = new();

        /// <summary>
        /// Lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene tutti i blocchi
        /// </summary>
        private List<IFrontEndBlock> _allBlocks = new();


        /// <summary>
        /// Lista di tipo <see cref="List{IFrontEndBlock}"/> che contiene effettivamente i blocchi mostrati all'utente, 
        /// che possono essere trascinati
        /// </summary>
        private List<IFrontEndBlock> _blocks = new();

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
        private List<IFrontEndBlock> _droppedBlocks = new();

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
                _allBlocks.Add(block.GetInfo());
            Blocks = _allBlocks.OrderBy(block => block.Descriptor.Category).ToList();
        }

        /// <summary>
        /// Verifica se un blocco può essere rilasciato nel punto passato come parametro
        /// </summary>
        /// <param name="dropped"> Blocco selezionato </param>
        /// <param name="dropPoint"> Punto di rilascio del blocco </param>
        /// <returns> true se il blocco puo' essere posizionato nel punto indicato </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool CanBeDropped(IFrontEndBlock dropped, PointF dropPoint)
        {
            var under = GetBlockFromPoint(dropPoint);

            if (dropped.Descriptor.Type is BlockType.Principale && DroppedBlocks.Exists(bl => dropped.Descriptor.Name.Equals(bl.Descriptor.Name)))
                throw new InvalidOperationException("Un blocco '" + dropped.Descriptor.Name.ToUpper() + "' è già stato posizionato.");
            if (under is null && dropped.Shape.Type is not ShapeType.UPPER)
                throw new InvalidOperationException("Il blocco '" + dropped.Descriptor.Name.ToUpper() + "' puo' essere posizionato solamente sotto ad una altro blocco");
            if (dropped.Descriptor.Type is BlockType.ChiamaFunzione && !FunctionNames.Any())
                throw new InvalidOperationException("Il blocco '" + dropped.Descriptor.Name.ToUpper() + "' puo' essere posizionato solamente dopo aver DEFINITO almeno 1 funzione.");
            if (dropped.Descriptor.Type is BlockType.ModificaVariabile && !VariableNames.Any())
                throw new InvalidOperationException("Il blocco '" + dropped.Descriptor.Name.ToUpper() + "' puo' essere posizionato solamente dopo aver DEFINITO almeno 1 variabile.");

            return true;
        }

        /// <summary>
        /// Metodo che permette di aggiungere, alla lista dei blocchi trascinati, un nuovo blocco <see cref="IFrontEndBlock"/>
        /// </summary>
        /// <param name="dropped"> Blocco selezionato da aggiungere alla lista </param>
        public void AddDroppedBlock(IFrontEndBlock dropped, PointF dropPoint)
        {
            var underBlock = DroppedBlocks.Where(block => Contains(block, dropPoint)).LastOrDefault();

            if (dropped.Descriptor.Type is BlockType.DefinizioneFunzione) FunctionNames.Add(dropped.Questions.ElementAt(2).Value);
            if (dropped.Descriptor.Type is BlockType.DefinizioneVariabile) VariableNames.Add(dropped.Questions.ElementAt(1).Value);
            if (dropped.Shape.Type is ShapeType.UPPER)
            {
                dropped.Position.UpperLeft = GetStartPosition(dropPoint);
            }
            else
            {
                if (underBlock == null) return;
                ShiftBlocksWhenDropped(dropped, SetUpperLeft(new(dropPoint.X, dropPoint.Y), dropped, underBlock));
            }
            DroppedBlocks = DroppedBlocks.Append(dropped).OrderBy(b => b.Position.UpperLeft.Y).ToList();
        }

        private PointF GetStartPosition(PointF originalPosition)
        {
            IFrontEndBlock? bl;
            while ((bl = GetBlockFromPoint(originalPosition)) != null)
                originalPosition.X += (bl.Position.BottomRight.X - originalPosition.X) + 20;

            return originalPosition;
        }

        /// <summary>
        /// Imposta le coordinate del punto in alto a sinistra del blocco trascinato, in base al punto di rilascio e al blocco 
        /// che contiene quest'ultimo
        /// </summary>
        /// <param name="originalPosition"> Posizione di rilascio del blocco </param>
        /// <param name="dropped"> Blocco trascinato </param>
        /// <param name="under"> Eventuale blocco contenente il punto di rilascio </param>
        /// <returns> Il blocco dal quale shiftare </returns>
        private IFrontEndBlock? SetUpperLeft(PointF originalPosition, IFrontEndBlock dropped, IFrontEndBlock under)
        {
            IFrontEndBlock? returnBlock;
            IFrontEndBlock? start = under.CanContainChildren ? under : under.Father;
            PointF upperCorner = (start is null) ? new() : new(start.Position.UpperLeft.X, start.Position.UpperLeft.Y + 40);
            PointF bottomCorner = (start is null) ? new() : new(start.Position.BottomRight.X, start.Position.BottomRight.Y - 40);
            if (start != null && start.CanContainChildren && Contains(upperCorner, bottomCorner, originalPosition))
            {
                var lastChildren = under.Children.LastOrDefault();
                originalPosition = GetChildPosition(lastChildren, upperCorner, originalPosition);
                returnBlock = ResizeParent(under, dropped.Shape.BlockOffset.Y);
                dropped.Father = under;
                if (lastChildren != null)
                {
                    lastChildren.Next = dropped;
                }

                under.Children.Add(dropped);
                under.Children.OrderBy(b => b.Position.UpperLeft.Y);
            }
            else
            {
                ResizeParent(under.Father, dropped.Shape.BlockOffset.Y);
                returnBlock = under.Next;
                originalPosition.X = under.Position.UpperLeft.X;
                originalPosition.Y = under.Position.UpperLeft.Y + under.Shape.BlockOffset.Y;
                dropped.Father = under.Father ?? under;
                dropped.Next = under.Next;
                under.Next = dropped;
                dropped.Father.Children.Add(dropped);
                dropped.Father.Children.OrderBy(b => b.Position.UpperLeft.Y);
            }

            dropped.Position.UpperLeft = originalPosition;
            return returnBlock ?? dropped.Next;
        }

        private IFrontEndBlock? ResizeParent(IFrontEndBlock? current, float offset)
        {
            while (current?.Shape.Type is ShapeType.WITH_CHILDREN)
            {
                current.Height += offset;
                current.Shape.BlockOffset = new(current.Shape.BlockOffset.X, current.Shape.BlockOffset.Y + offset);
                if (current.Father == null || current.Father.Shape.Type != ShapeType.WITH_CHILDREN) break;
                current = current.Father;
            }

            return current?.Next;
        }
        private PointF GetChildPosition(IFrontEndBlock? lastChildren, PointF upperCorner, PointF originalPosition)
        {
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
            return originalPosition;
        }

        /// <summary>
        /// Elimina il blocco passato come parametro
        /// </summary>
        /// <param name="deletedBlock"> Blocco da eliminare </param>
        public void DeleteDroppedBlock(IFrontEndBlock deletedBlock)
        {
            List<IFrontEndBlock> toBeRemoved = new(deletedBlock.Children);

            if (deletedBlock.Father != null)
            {
                if (deletedBlock.Father.Next == deletedBlock) deletedBlock.Father.Next = deletedBlock.Next;
                if (deletedBlock.Father.CanContainChildren)
                {
                    deletedBlock.Father.Height -= deletedBlock.Shape.BlockOffset.Y;
                }
                deletedBlock.Father.Children.Remove(deletedBlock);
            }

            if (deletedBlock.Descriptor.Type is BlockType.DefinizioneFunzione)
            {
                var functionName = deletedBlock.Questions.ElementAt(0).Value;
                RemoveFunctionUse(functionName);
            }

            toBeRemoved.Add(deletedBlock);
            toBeRemoved.ForEach(block =>
            {
                ShiftBlocksWhenDelete(block);
                DroppedBlocks.Remove(block);
            });
            DroppedBlocks = DroppedBlocks.Where(x => !x.Equals(deletedBlock)).ToList();
        }

        private void RemoveFunctionUse(string functionName)
        {
            FunctionNames.Remove(functionName);
            IEnumerable<IFrontEndBlock> toRemove = DroppedBlocks.Where(block => block.Descriptor.Type.Equals(BlockType.ChiamaFunzione) && block.Questions.ElementAt(0).Value.Equals(functionName));
            foreach (var callBlock in toRemove)
            {
                callBlock.Father?.Children.Remove(callBlock);
            }
        }

        /// <summary>
        /// Sposta i blocchi quando un nuovo blocco viene rilasciato
        /// </summary>
        /// <param name="droppedBlock"> Blocco rilasciato </param>
        /// <param name="underBlock"> Blocco contenente il punto di rilascio </param>
        private void ShiftBlocksWhenDropped(IFrontEndBlock droppedBlock, IFrontEndBlock? underBlock)
        {
            if (droppedBlock.Descriptor.Type != BlockType.Principale && droppedBlock.Descriptor.Type != BlockType.DefinizioneFunzione)
                Shift(underBlock, droppedBlock.Shape.BlockOffset.Y, (x, y) => x + y);
        }
        /// <summary>
        /// Sposta i blocchi quando un blocco, precedentemente rilasciato, viene eliminato
        /// </summary>
        /// <param name="deletedBlock"> Blocco eliminato </param>
        private void ShiftBlocksWhenDelete(IFrontEndBlock deletedBlock)
        {
            Shift(deletedBlock, deletedBlock.Shape.BlockOffset.Y, (x, y) => x - y);
        }
        /// <summary>
        /// Sposta i blocchi, a partire da quello posizionato sotto al blocco di partenza passato come parametro
        /// </summary>
        /// <param name="currentBlock"> Blocco di partenza </param>
        /// <param name="offset"> Offset che indica di quanto i blocchi vadano spostati </param>
        /// <param name="YSetterFunction"> Funzione per impostare le coordinate del punto in alto a sinistra del blocco da spostare </param>
        private void Shift(IFrontEndBlock? currentBlock, float offset, Func<float, float, float> YSetterFunction)
        {
            var current = currentBlock;
            IFrontEndBlock? next = current;

            while (next != null)
            {
                foreach (var child in next.Children)
                    child.Position.UpperLeft = new(child.Position.UpperLeft.X, YSetterFunction.Invoke(child.Position.UpperLeft.Y, offset));

                current = next;
                next = current.Next;
                current.Position.UpperLeft = new(current.Position.UpperLeft.X, YSetterFunction.Invoke(current.Position.UpperLeft.Y, offset));
            }
        }

        /// <summary>
        /// Restitusce un blocco contenente il punto passato come parametro, o null se questo non esiste
        /// </summary>
        /// <param name="pointF"> Punto dal quale estarre un blocco </param>
        /// <returns> un blocco contenente il punto passato come parametro, o null se questo non esiste </returns>
        public IFrontEndBlock? GetBlockFromPoint(PointF pointF)
        {
            return DroppedBlocks.Where(block => Contains(block, pointF)).LastOrDefault();
        }

        /// <summary>
        /// Verifica se un blocco contiene il punto passato come parametro
        /// </summary>
        /// <param name="block"> Blocco con il quale effettuare la verifica </param>
        /// <param name="point"> Punto del quale verificare l'appartenenza al blocco </param>
        /// <returns> se il blocco contiene il punto passato come parametro, false altrimenti </returns>
        public bool Contains(IFrontEndBlock block, PointF point)
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
        public bool Contains(PointF upperCorner, PointF bottomCorner, PointF point)
        {
            if (upperCorner.X > point.X || bottomCorner.X < point.X) return false;
            if (upperCorner.Y > point.Y || bottomCorner.Y < point.Y) return false;
            return true;
        }

        /// <summary>
        /// Metodo che aggiorna la lista dei blocchi mostrati all'utente in base al tipo
        /// </summary>
        /// <param name="category"> Tipo di blocco in base al quale filtrare la lista </param>
        public void UpdateBlocksByCategory(BlockCategory category)
        {
            Blocks = _allBlocks.FindAll((e) => e.Descriptor.Category.Equals(category));
        }

        /// <summary>
        /// Restituisce una stringa, in formato Json, che rappresenta i blocchi posizionati
        /// </summary>
        /// <returns> una stringa in formato Json </returns>
        public string GetJsonDroppedBlocks()
        {
            List<FEBlockSerializable> list = new();

            foreach (var item in DroppedBlocks)
                list.Add(new FEBlockSerializable(item));

            return JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                WriteIndented = true
            });
        }

        /// <summary>
        /// Imposta la lista dei blocchi posizionati a partire da una stringa formato Json passata come parametro
        /// </summary>
        /// <param name="serializedDroppedBlocks"> Stringa, in formato Json, che rappresenta i blocchi posizionati </param>
        public void SetDroppedBlocksFromJson(string serializedDroppedBlocks)
        {
            List<IFrontEndBlock> deserializedBlocks = new();
            List<FEBlockSerializable> serializedBlocks = JsonSerializer.Deserialize<List<FEBlockSerializable>>(serializedDroppedBlocks) ?? new();

            foreach (var serialized in serializedBlocks)
            {
                Type? blockType = Type.GetType(serialized.BlockType ?? "");
                if (blockType != null)
                {
                    IFrontEndBlock? baseBlock = (IFrontEndBlock?)Activator.CreateInstance(blockType);

                    if (baseBlock != null)
                    {
                        IFrontEndBlock deSerialized = baseBlock.GetInfo();
                        deSerialized.Position = serialized.Position;
                        deSerialized.Descriptor = new BlockDescriptor(serialized.DescriptorName, serialized.DescriptorType, serialized.DescriptorCategory);
                        serialized.Questions.ForEach(question => deSerialized.Questions.ElementAt(question.Item1).SetValue(question.Item2));
                        deserializedBlocks.Add(deSerialized);
                    }
                }
            }

            DroppedBlocks = deserializedBlocks;
        }
    }
}