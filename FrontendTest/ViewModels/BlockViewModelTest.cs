using Frontend.Models.Blocks;
using Frontend.Models.Blocks.ConcreteBlocks;
using Frontend.Models.GraphicViews;
using Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendTest.ViewModels
{
    public class BlockViewModelTest
    {
        private BlockViewModel _view = new BlockViewModel(new());


        [Fact]
        public void ShouldThrowIfBlockCannotBePlace()
        {
            _view.AddDroppedBlock(new StartBlock().GetInfo(), new(4, 4));
            var exceptionType = new InvalidOperationException().GetType();
            Assert.Throws(exceptionType, () => _view.CanBeDropped(new StartBlock().GetInfo(), new PointF(5, 5)));
            Assert.Throws(exceptionType, () => _view.CanBeDropped(new MovementForwardBlock().GetInfo(), new PointF(5, 5)));
            Assert.Throws(exceptionType, () => _view.CanBeDropped(new FunctionCallBlock().GetInfo(), new PointF(5, 5)));
            Assert.Throws(exceptionType, () => _view.CanBeDropped(new SetVariableBlock().GetInfo(), new PointF(5, 5)));
        }

        [Fact]
        public void ShouldNotThrowIFBlockCanBePlace()
        {
            _view.AddDroppedBlock(new StartBlock().GetInfo(), new(4, 4));
            Assert.True(_view.CanBeDropped(new MovementForwardBlock().GetInfo(), new PointF(4, 4)));
        }

        [Theory]
        [InlineData("Func1")]
        [InlineData("Func2")]
        [InlineData("Func3")]
        public void ShouldAddFunctionName(string funcName)
        {
            IFrontEndBlock functionDefinition = new FunctionDefinitionBlock().GetInfo();
            functionDefinition.Questions[2].SetValue(funcName);
            _view.AddDroppedBlock(functionDefinition, new(4, 4));
            Assert.Contains(funcName, BlockViewModel.FunctionNames);
        }
        [Theory]
        [InlineData("var1")]
        [InlineData("var2")]
        [InlineData("var3")]
        public void ShouldAddVariableName(string varName)
        {
            IFrontEndBlock varDefinition = new VariableDefinitionBlock().GetInfo();
            varDefinition.Questions[2].SetValue(varName);
            _view.AddDroppedBlock(varDefinition, new(4, 4));
            Assert.Contains(varName, BlockViewModel.VariableNames);
        }

        [Theory]
        [InlineData("Func1")]
        [InlineData("Func2")]
        [InlineData("Func3")]
        public void ShouldRemoveFunctionName(string funcName)
        {
            IFrontEndBlock functionDefinition = new FunctionDefinitionBlock().GetInfo();
            functionDefinition.Questions[2].SetValue(funcName);
            _view.AddDroppedBlock(functionDefinition, new(4, 4));
            _view.AddDroppedBlock(new StartBlock().GetInfo(), new(7, 7));
            IFrontEndBlock functionCall = new FunctionCallBlock().GetInfo();
            functionCall.Questions[0].SetValue(funcName);
            _view.AddDroppedBlock(functionCall, new(7, 7));
            _view.DeleteDroppedBlock(functionDefinition);
            Assert.DoesNotContain(funcName, BlockViewModel.FunctionNames);
            Assert.DoesNotContain(functionCall, _view.DroppedBlocks);
        }
        [Theory]
        [InlineData("var1")]
        [InlineData("var2")]
        [InlineData("var3")]
        public void ShouldRemoveVariableName(string varName)
        {
            IFrontEndBlock varDefinition = new VariableDefinitionBlock().GetInfo();
            varDefinition.Questions[2].SetValue(varName);
            _view.AddDroppedBlock(varDefinition, new(4, 4));
            IFrontEndBlock variableSet = new SetVariableBlock().GetInfo();
            variableSet.Questions[0].SetValue(varName);
            _view.AddDroppedBlock(variableSet, new(4, 4));
            _view.DeleteDroppedBlock(varDefinition);
            Assert.DoesNotContain(varName, BlockViewModel.VariableNames);
            Assert.DoesNotContain(variableSet, _view.DroppedBlocks);
        }

        [Fact]
        public void ShouldGetBlockByPointIfExist()
        {
            PointF point = new(4, 4);
            IFrontEndBlock block = new StartBlock().GetInfo();
            _view.AddDroppedBlock(block, point);
            Assert.Equal(block, _view.GetBlockFromPoint(point));
        }

        [Fact]
        public void ShouldGetNullIfPointNotExist()
        {
            PointF point = new(4, 4);
            Assert.Null(_view.GetBlockFromPoint(point));
        }

        [Fact]
        public void ShouldGenerateCorrectJsonString()
        {
            string correctJson = "[\r\n  {\r\n    \"Path\": \"m 0,4 A 4,4 0 0,1 4,0 H 50 c 2,0 3,1 4,2 v 15 H 160 a 4,4 0 0,1 4,4 v 48 a 4,4 0 0,1 -4,4 H 48   c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 H 4 a 4,4 0 0,1 -4,-4 z\",\r\n    \"Questions\": [],\r\n    \"DescriptorName\": \"Start\",\r\n    \"DescriptorType\": 0,\r\n    \"DescriptorCategory\": 0,\r\n    \"Position\": {\r\n      \"Width\": 164,\r\n      \"Height\": 81,\r\n      \"UpperLeft\": {\r\n        \"X\": 259.99997,\r\n        \"Y\": 108.799995,\r\n        \"IsEmpty\": false\r\n      },\r\n      \"BottomRight\": {\r\n        \"X\": 423.99997,\r\n        \"Y\": 189.79999,\r\n        \"IsEmpty\": false\r\n      }\r\n    },\r\n    \"BlockType\": \"Frontend.Models.Blocks.ConcreteBlocks.StartBlock\"\r\n  }\r\n]";
            var block = new StartBlock().GetInfo();
            block.Draw(new PictureCanvas(70, 70, 70, 70));
            _view.AddDroppedBlock(block, new(259.99997f, 108.799995f));
            Assert.Equal(correctJson, _view.GetJsonDroppedBlocks());
        }

        [Fact]
        public void ShouldLoadCorrectJsonString()
        {
            string json = "[\r\n  {\r\n    \"Path\": \"m 0,4 A 4,4 0 0,1 4,0 H 50 c 2,0 3,1 4,2 v 15 H 160 a 4,4 0 0,1 4,4 v 48 a 4,4 0 0,1 -4,4 H 48   c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 H 4 a 4,4 0 0,1 -4,-4 z\",\r\n    \"Questions\": [],\r\n    \"DescriptorName\": \"Start\",\r\n    \"DescriptorType\": 0,\r\n    \"DescriptorCategory\": 0,\r\n    \"Position\": {\r\n      \"Width\": 164,\r\n      \"Height\": 80.99999,\r\n      \"UpperLeft\": {\r\n        \"X\": 259.99997,\r\n        \"Y\": 108.799995,\r\n        \"IsEmpty\": false\r\n      },\r\n      \"BottomRight\": {\r\n        \"X\": 423.99997,\r\n        \"Y\": 189.79999,\r\n        \"IsEmpty\": false\r\n      }\r\n    },\r\n    \"BlockType\": \"Frontend.Models.Blocks.ConcreteBlocks.StartBlock\"\r\n  }\r\n]";
            _view.SetDroppedBlocksFromJson(json);
            Assert.True(_view.DroppedBlocks.Where(x => x.Descriptor.Type.Equals(BlockType.Principale)).Count() == 1);
            Assert.True(_view.DroppedBlocks.Where(x => x.Position.UpperLeft.X == 259.99997f && x.Position.UpperLeft.Y == 108.799995f).Count() == 1);
        }
    }
}
