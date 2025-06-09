using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Maui.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ToDoServiceProxy _toDoSericeProxy;

        public MainPageViewModel()
        {
            _toDoSericeProxy = ToDoServiceProxy.Current;
        }

        public ObservableCollection<ToDo> ToDos
        {
            get
            {
                var toDos = _toDoSericeProxy.ToDos;
                if(!IsShowCompleted)
                {
                    toDos = _toDoSericeProxy.ToDos.Where(t => !t?.IsCompleted ?? false).ToList();
                }
                return new ObservableCollection<ToDo>(toDos);
            }
        }

        private bool isShowCompleted;
        public bool IsShowCompleted { 
            get
            {
                return isShowCompleted;
            }

            set
            {
                if (isShowCompleted != value)
                {
                    isShowCompleted = value;
                    NotifyPropertyChanged(nameof(ToDos));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
