using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Algodat_AVLTree;

namespace AVLTreeVisualizer
{
    public class TreeViewModel
    {
        private ObservableCollection<int> nodes;
        private AVLTree tree;

        public TreeViewModel()
        {
            this.tree = new AVLTree();
            this.Nodes = new ObservableCollection<int>();
        }

        public MyCommand AddValueCommand
        {
            get
            {
                return new MyCommand(p =>
                {
                    bool valid = int.TryParse(p.ToString(), out int parsedValue);

                    if (!valid)
                        throw new ArgumentException(nameof(p), "Please specify a number to insert");

                    this.tree.Insert(parsedValue);
                    this.Nodes = new ObservableCollection<int>(this.tree.Traverse(TraverseOrder.InOrder));
                },
                p => true);
            }
        }

        public int HeadNode
        {
            get
            {
                return this.tree.HeadNode.Content;
            }
        }

        public ObservableCollection<int> Nodes
        {
            get
            {
                return this.nodes;
            }

            set
            {
                this.nodes = value;
                this.RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName]string property = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
