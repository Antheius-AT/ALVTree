using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Algodat_AVLTree;

namespace AVLTreeVisualizer
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            this.TreeViewModel = new TreeViewModel();
        }

        public TreeViewModel TreeViewModel
        {
            get;
            set;
        }
    }
}
