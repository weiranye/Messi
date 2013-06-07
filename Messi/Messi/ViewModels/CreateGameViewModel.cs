using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messi.ViewModels
{
    public class CreateGameViewModel
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Word { get; set; }
        public string Definition { get; set; }

    }
    public class ListCreateGameViewModel
    {
        public List<CreateGameViewModel> CreateGameViewModels { get; set; } 
    }
}