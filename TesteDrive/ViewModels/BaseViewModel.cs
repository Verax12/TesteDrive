﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TesteDrive.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // UTILIZA DESTA FORMA O PROPERTYCHANGED NAS VIEW MODELS 
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
