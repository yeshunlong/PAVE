using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WorkflowManager.Core.Models;

namespace WorkflowManager.UI.UserControls
{
    public partial class TaskCard : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TaskProperty =
    DependencyProperty.Register("Task", typeof(Task), typeof(TaskCard), new PropertyMetadata(null));

        public Task Task
        {
            get { return (Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public TaskCard()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
