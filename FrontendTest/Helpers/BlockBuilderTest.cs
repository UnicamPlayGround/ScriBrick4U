
using Frontend.Helpers.Builders;
using Frontend.Models.Blocks;
using Frontend.Models.Blocks.ConcreteBlocks;
using Frontend.Models.QuestionItem;

namespace FrontendTest.Helpers
{
    public class BlockBuilderTest
    {
        private IBlockBuilder<MovementForwardBlock> _builder = new BlockBuilder<MovementForwardBlock>("forward_test", BlockType.Movimento, BlockCategory.Movimento);

        [Theory]
        [InlineData(BlockType.Movimento)]
        [InlineData(BlockType.Evento)]
        [InlineData(BlockType.DefinizioneFunzione)]
        [InlineData(BlockType.DefinizioneVariabile)]
        public void ShouldBuildCorrectBlockType(BlockType type)
        {
            var _builder = new BlockBuilder<MovementBackwardBlock>("test", type, BlockCategory.Movimento);
            Assert.Equal(_builder.Build().Descriptor.Type, type);
        }
        [Theory]
        [InlineData(BlockCategory.Variabile)]
        [InlineData(BlockCategory.Movimento)]
        [InlineData(BlockCategory.Evento)]
        [InlineData(BlockCategory.Controllo)]
        public void ShouldBuildCorrectBlockCategory(BlockCategory category)
        {
            var _builder = new BlockBuilder<MovementBackwardBlock>("test", BlockType.Principale, category);
            Assert.Equal(_builder.Build().Descriptor.Category, category);
        }
        [Theory]
        [InlineData("label1")]
        [InlineData("label2")]
        [InlineData("label3")]
        public void ShouldAddCorrectLabel(string value)
        {
            IFrontEndBlock result = _builder.AddLabel(value).Build();
            Assert.Equal(value, ((Label)result.Elements[0]).Text);
        }

        [Theory]
        [InlineData("question1")]
        [InlineData("question2")]
        [InlineData("question3")]
        public void ShouldAddCorrectQuestions(string value)
        {
            IFrontEndBlock result = _builder.AddQuestion(new EntryEditItem(value)).Build();
            Assert.True(result.Questions[0].Element is Entry);
            Assert.Equal(value, result.Questions[0].Question.Text);
        }

        [Fact]
        public void ShouldBuildCorrectType()
        {
            Assert.True(_builder.Build() is MovementForwardBlock);
        }
    }
}
