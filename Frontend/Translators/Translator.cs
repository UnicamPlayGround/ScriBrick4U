using Backend.Blocks;
using Backend.Blocks.Conditional;
using Backend.Blocks.Events;
using Backend.Blocks.Movement;
using Backend.Blocks.Operation;
using Backend.Blocks.Starts;
using Backend.Blocks.Value;
using Backend.Blocks.Variable;
using Frontend.Model.Blocks;

namespace Frontend.Translators
{
    public class Translator : ITranslator
    {
        private int Counter = 0;
        private IDictionary<string, List<IBlock>> functions = new Dictionary<string, List<IBlock>>();
        private List<IFrontEndBlock> frontEndFunctions = new ();

        public IEnumerable<IBlock> Translate(IEnumerable<IFrontEndBlock> frontEndBlocks)
        {
            frontEndFunctions.AddRange(frontEndBlocks.Where(x => x.Descriptor.Type.Equals(BlockType.DefinizioneFunzione)));
            TranslateAllFunctions();

            List<IBlock> blocks = new();
            foreach (IFrontEndBlock start in frontEndBlocks.Where(x => x.Descriptor.Type.Equals(BlockType.Principale)))
            {
                IBlock block = Creator(start);
                block.Children = TranslateChildren(start.Children);
                blocks.Add(block);
            }
            return blocks;
        }
        private void TranslateAllFunctions()
        {
            foreach (IFrontEndBlock function in frontEndFunctions)
            {
                if (!functions.ContainsKey(function.Questions[0].Value))
                {
                    TranslateFunction(function);
                }
            }
        }

        private void TranslateFunction(IFrontEndBlock function)
        {
            foreach (IFrontEndBlock child in function.Children)
            {
                if (child.Descriptor.Type.Equals(BlockType.ChiamaFunzione) && !functions.ContainsKey(child.Questions[0].Value))
                {
                    TranslateFunction(frontEndFunctions.Single(x => x.Questions[0].Value.Equals(child.Questions[0].Value)));
                }
            }
            functions.Add(function.Questions[0].Value, TranslateChildren(function.Children));
        }

        private List<IBlock> TranslateChildren(IEnumerable<IFrontEndBlock> children)
        {
            List<IBlock> backEndChildren = new();
            foreach (IFrontEndBlock child in children)
            {
                if (child.Descriptor.Type.Equals(BlockType.ChiamaFunzione))
                {
                    backEndChildren.AddRange(functions[child.Questions[0].Value]);
                }
                else
                {
                    IBlock block = Creator(child);
                    if (child.Children.Count > 0)
                    {
                        block.Children = TranslateChildren(child.Children);
                    }
                    backEndChildren.Add(block);
                }
            }
            return backEndChildren;
        }

        private IBlock Creator(IFrontEndBlock frontEndBlock)
        {
            IBlock block = null;
            switch (frontEndBlock.Descriptor.Type)
            {
                case BlockType.Principale:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "Start":
                            block = new StartBlock();
                            break;
                        case "Update":
                            block = new UpdateBlock();
                            break;
                    }
                    break;
                case BlockType.Movimento:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "Movimento":
                            block = new ForwardBlock($"Forward{Counter++}", new ValueBlock($"Variable{Counter++}", int.Parse(frontEndBlock.Questions[0].Value)));
                            break;
                        case "Rotazione":
                            block = new RotationBlock($"Forward{Counter++}", new ValueBlock($"Variable{Counter++}", int.Parse(frontEndBlock.Questions[0].Value)));
                            break;
                    }
                    break;
                case BlockType.Operazionale:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "Operazione":
                            block = new OperationBlock(
                                $"Addition{Counter++}",
                                getValueBlock(frontEndBlock.Questions[0].Value),
                                frontEndBlock.Questions[1].Value,
                                getValueBlock(frontEndBlock.Questions[2].Value)
                                );
                            break;
                    }
                    break;
                case BlockType.Condizionale:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "If":
                            block = new IfBlock(
                                $"If{Counter++}",
                                getVariableBlock(frontEndBlock.Questions[0].Value),
                                frontEndBlock.Questions[1].Value,
                                getVariableBlock(frontEndBlock.Questions[2].Value));
                            break;
                    }
                    break;
                case BlockType.Evento:
                    switch (frontEndBlock.Descriptor.Name) {
                        case "KeyboardEvent":
                            block = new KeyboardEvent(
                                $"Event{Counter++}",
                                frontEndBlock.Questions[0].Value
                            );
                            break;
                    }
                    break;
            }
            if(block == null)
            {
                throw new NotImplementedException(frontEndBlock.Descriptor.Name);
            }
            return block;
        }


        private ValueBlock getValueBlock(string value)
        {
            return new ValueBlock($"Value{Counter++}", int.Parse(value));
        }
    }
}
