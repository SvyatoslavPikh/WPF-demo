﻿using System;
using System.Windows.Input;

namespace WpfDemo.Library
{
    public class ActionCommand : ICommand
    {
        private readonly Action<Object> _action;
        private readonly Predicate<object> _predicate;

        public ActionCommand(Action<Object> action) : this(action, null)
        {
            
        }

        public ActionCommand(Action<Object> action, Predicate<object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "You must specify an Action<T>");
            }
            _action = action;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            if (_predicate == null)
            {
                return true;
            }
            return _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public void Execute()
        {
            Execute(null);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}