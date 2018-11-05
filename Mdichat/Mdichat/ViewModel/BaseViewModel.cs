﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MdiChat.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation


        public Action PageLoaded;
        public Action ShowLoadingView;
        public Action HideLoadingView;

        public INavigation Navigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isBusy; 
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");
            var body = selectorExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The body must be a member expression");
            RaisePropertyChanged(body.Member.Name);
        }

        protected virtual void OnAllPropertiesChanged()
        {
            RaisePropertyChanged(string.Empty);
        }

        #endregion

    }
}
