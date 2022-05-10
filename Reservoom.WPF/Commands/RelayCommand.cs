﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservoom.WPF.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object?, bool>? _canExecute;
        private readonly Action<object?> _execute;

        public RelayCommand(Func<object?, bool>? canExecute, Action<object?> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public RelayCommand(Action<object?> execute)
        {
            _execute = execute;
            _canExecute = null;
        }

        public bool CanExecute(object? parameter)
        {
            
                return _canExecute == null || _canExecute(parameter);
            
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
