using Backend.Blocks;
using Backend.Blocks.Conditional;
using Backend.Blocks.Events;
using Backend.Blocks.Function;
using Backend.Blocks.Movement;
using Backend.Blocks.Operation;
using Backend.Blocks.Starts;
using Backend.Blocks.Text;
using Backend.Blocks.Variable;
using Frontend.Models.Blocks;

namespace Frontend.Translators
{
    public class Translator : ITranslator
    {
        private int _counter = 0;

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
                        case "Collision":
                            block = new ColliderBlock();
                            break;
                    }
                    break;
                case BlockType.Movimento:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "MovimentoAvanti":
                            block = new ForwardBlock($"Forward{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "MovimentoIndietro":
                            block = new BackwardBlock($"Backward{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "MovimentoSu":
                            block = new UpBlock($"Up{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "MovimentoGiu":
                            block = new DownBlock($"Down{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "Rotazione":
                            block = new RotationBlock($"Rotation{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "Gravita":
                            block = new GravityBlock($"Gravity{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "Velocita":
                            block = new SpeedBlock($"Speed{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                        case "Posizione":
                            block = new PositionBlock($"Position{_counter++}", frontEndBlock.Questions[0].Value, frontEndBlock.Questions[1].Value);
                            break;
                    }
                    break;
                case BlockType.Operazionale:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "Operazione":
                            block = new OperationBlock(
                                $"Addition{_counter++}",
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
                                $"If{_counter++}",
                                frontEndBlock.Questions[0].Value ?? frontEndBlock.Questions[1].Value,
                                frontEndBlock.Questions[2].Value,
                                frontEndBlock.Questions[3].Value ?? frontEndBlock.Questions[4].Value
                            );
                            break;
                        case "While":
                            block = new WhileBlock(
                                $"While{_counter++}",
                                frontEndBlock.Questions[0].Value ?? frontEndBlock.Questions[1].Value,
                                frontEndBlock.Questions[2].Value,
                                frontEndBlock.Questions[3].Value ?? frontEndBlock.Questions[4].Value
                            );
                            break;
                        case "For":
                            block = new ForBlock(
                                $"For{_counter++}",
                                frontEndBlock.Questions[0].Value
                            );
                            break;
                        case "CollisionWith":
                            block = new CollisionWithBlock(
                                $"CollisionWith{_counter++}",
                                frontEndBlock.Questions[0].Value);
                            break;
                        case "Wait":
                            block = new WaitBlock($"Wait{_counter++}", frontEndBlock.Questions[0].Value);
                            break;
                    }
                    break;
                case BlockType.Evento:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "KeyboardEvent":
                            block = new KeyboardEvent(
                                $"Event{_counter++}",
                                frontEndBlock.Questions[0].Value
                            );
                            break;
                    }
                    break;
                case BlockType.DefinizioneFunzione:
                    block = new FunctionDefinitionBlock(
                        $"DefinitionFunction{_counter++}",
                        frontEndBlock.Questions[0].Value,
                        frontEndBlock.Questions[1].Value,
                        frontEndBlock.Questions[2].Value);
                    break;
                case BlockType.ChiamaFunzione:
                    block = new FunctionCallBlock(
                        $"CallFunction{_counter++}",
                        frontEndBlock.Questions[0].Value,
                        frontEndBlock.Questions[1].Value);
                    break;
                case BlockType.RitornaValore:
                    block = new ReturnBlock(
                        $"ReturnBlock{_counter++}",
                        frontEndBlock.Questions[0].Value ?? frontEndBlock.Questions[1].Value
                    );
                    break;
                case BlockType.DefinizioneVariabile:
                    block = new VariableBlock(
                        $"Variable{_counter++}", 
                        frontEndBlock.Questions[0].Value,
                        frontEndBlock.Questions[1].Value,
                        frontEndBlock.Questions[2].Value);
                    break;
                case BlockType.ModificaVariabile:
                    block = new SetVariableBlock(
                               $"SetVariable{_counter++}",
                               frontEndBlock.Questions[0].Value,
                               frontEndBlock.Questions[1].Value,
                               frontEndBlock.Questions[2].Value);
                    break;
                case BlockType.Text:
                    switch (frontEndBlock.Descriptor.Name)
                    {
                        case "Testo":
                            var text = frontEndBlock.Questions[1].Value;
                            if (string.IsNullOrEmpty(text))
                            {
                                text = frontEndBlock.Questions[2].Value;
                            }
                            else
                            {
                                text = $"\"{text}\"";
                            }
                            block = new TextBlock($"Text{_counter++}", frontEndBlock.Questions[0].Value, text);
                            break;
                    }
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
