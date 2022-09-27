using Frontend.Models.Blocks;
using Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendTest.ViewModels
{
    public class BlockTypeViewModelTest
    {
        BlockTypeViewModel _view = new BlockTypeViewModel();

        [Theory]
        [InlineData(BlockCategory.Variabile)]
        [InlineData(BlockCategory.Evento)]
        public void ShouldFilterCategory(BlockCategory category)
        {
            _view.SetSelectedCategory(category);
            Assert.Equal(_view.SelectedCategory, category);
        }
    }
}
