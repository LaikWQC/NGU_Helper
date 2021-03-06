﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NGU_Helper.Utils
{    
    public abstract class DialogBase
    {
        private readonly Window _window;
        private readonly bool _closeWarning;

        public DialogBase(Window owner, string title, bool closeWarning = true)
        {
            _closeWarning = closeWarning;
            _window = new Window
            {
                Title = title,
                Owner = owner,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize                
            };            
            _window.KeyDown += KeyDown;
            _window.Closing += OnClosing;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (_closeWarning && IsChanged)
            {
                MessageBoxResult result = MessageBox.Show(_window, "Close without saving?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Escape:
                    _window.Close();
                    break;
                case Key.Enter:
                    if (OkCommand?.CanExecute(null) ?? false)
                        OkCommand.Execute(null);
                    break;
            }
        }

        public void ShowDialog()
        {
            _window.Content = ViewContent;
            _window.ShowDialog();
        }

        public void CloseWindow()
        {
            _window.Close();
        }

        public abstract ContentControl ViewContent { get; }
        public bool IsChanged { get; set; }
        public ICommand OkCommand { get; set; }
    }
}
