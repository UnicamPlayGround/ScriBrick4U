using Backend.Blocks;
using Backend.Blocks.Conditional;
using Backend.Blocks.Events;
using Backend.Blocks.Function;
using Backend.Blocks.Movement;
using Backend.Blocks.Operation;
using Backend.Blocks.Starts;
using Backend.Blocks.Variable;
using Frontend.Models.Blocks;

namespace Frontend.Translators
{
    public class Translator : ITranslator
    {
        private int Counter = 0;

        public IEnumerable<IBlock> Translate(IEnumerable<IFrontEndBlock> frontEndBlocks)
        {
            List<IBlock> blocks = new();
            foreach (IFrontEndBlock start in frontEndBlocks.Where(x => (x.Descriptor.Type.Equals(BlockType.Principale)
            || x.Descriptor.Type.Equals(BlockType.DefinizioneFunzione))))
            {
                IBlock block = Creator(start);
                block.Children = TranslateChildren(start.Children);
                blocks.Add(block);
            }
            return blocks;
        }

        private List<IBlock> TranslateChildren(IEnumerable<IFrontEndBlock> children)
        {
            List<IBlock> backEndChildren = new();
            foreach (IFrontEndBlock child in children)
            {
                IBlock block = Creator(child);
                if (child.Children.Count > 0)
                {
                    block.Children = TranslateChildren(child.Children);
                }
                backEndChildren.Add(block);
            }
            return backEndChildren;
        }

        private IBlock Creator(IFrontEndBlock frontEndBlock)
        {
            IBlock? block = null;
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
                            block = new ForwardBlock($"Forward{Counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "Rotazione":
                            block = new RotationBlock($"Forward{Counter++}", frontEndBlock.Questions[0].Value);
                            break;
                    }
                    break;
                case BlockType.Operazionale:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "Operazione":
                            block = new OperationBlock(
                                $"Addition{Counter++}",
                                frontEndBlock.Questions[0].Value,
                                frontEndBlock.Questions[1].Value,
                                frontEndBlock.Questions[2].Value);
                            break;
                    }
                    break;
                case BlockType.Condizionale:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "If":
                            block = new IfBlock(
                                $"If{Counter++}",
                                frontEndBlock.Questions[0].Value ?? frontEndBlock.Questions[1].Value,
                                frontEndBlock.Questions[2].Value,
                                frontEndBlock.Questions[3].Value ?? frontEndBlock.Questions[4].Value
                            );
                            break;
                        case "While":
                            block = new WhileBlock(
                                $"While{Counter++}",
                                frontEndBlock.Questions[0].Value ?? frontEndBlock.Questions[1].Value,
                                frontEndBlock.Questions[2].Value,
                                frontEndBlock.Questions[3].Value ?? frontEndBlock.Questions[4].Value
                            );
                            break;
                        case "For":
                            block = new ForBlock(
                                $"For{Counter++}",
                                frontEndBlock.Questions[0].Value
                            );
                            break;
                    }
                    break;
                case BlockType.Evento:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "KeyboardEvent":
                            block = new KeyboardEvent(
                                $"Event{Counter++}",
                                frontEndBlock.Questions[0].Value
                            );
                            break;
                    }
                    break;
                case BlockType.DefinizioneFunzione:
                    block = new FunctionDefinitionBlock(
                        $"DefinitionFunction{Counter++}",
                        frontEndBlock.Questions[0].Value,
                        frontEndBlock.Questions[1].Value,
                        frontEndBlock.Questions[2].Value);
                    break;
                case BlockType.ChiamaFunzione:
                    block = new FunctionCallBlock(
                        $"CallFunction{Counter++}",
                        frontEndBlock.Questions[0].Value,
                        frontEndBlock.Questions[1].Value);
                    break;
                case BlockType.RitornaValore:
                    block = new ReturnBlock(
                        $"ReturnBlock{Counter++}",
                        frontEndBlock.Questions[0].Value ?? frontEndBlock.Questions[1].Value
                    );
                    break;
                case BlockType.DefinizioneVariabile:
                    block = new VariableBlock($"Variable{Counter++}", frontEndBlock.Questions[0].Value, frontEndBlock.Questions[1].Value);
                    break;
                case BlockType.ModificaVariabile:
                    block = new SetVariableBlock(
                               $"SetVariable{Counter++}",
                               frontEndBlock.Questions[0].Value,
                               frontEndBlock.Questions[1].Value,
                               frontEndBlock.Questions[2].Value);
                    break;
            }
            if (block == null)
            {
                throw new NotImplementedException(frontEndBlock.Descriptor.Name);
            }
            return block;
        }
    }
}
