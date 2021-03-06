﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Views.Base;
using MvvmCross.ViewModels;

namespace MvvmCross.Forms.Views
{
    public class MvxContentPage : MvxEventSourceContentPage, IMvxPage
    {
        public MvxContentPage()
        {
            this.AdaptForBinding();
        }

        public object DataContext
        {
            get
            {
                return BindingContext.DataContext;
            }
            set
            {
                base.BindingContext = value;
                BindingContext.DataContext = value;
            }
        }

        private IMvxBindingContext _bindingContext;
        public new IMvxBindingContext BindingContext
        {
            get
            {
                if (_bindingContext == null)
                    BindingContext = new MvxBindingContext(base.BindingContext);
                return _bindingContext;
            }
            set
            {
                _bindingContext = value;
            }
        }

        public IMvxViewModel ViewModel
        {
            get
            {
                return DataContext as IMvxViewModel;
            }
            set 
            {
                DataContext = value;
                OnViewModelSet();
            }
        }

		protected virtual void OnViewModelSet()
		{
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel?.ViewDisappearing();
            ViewModel?.ViewDisappeared();
        }
    }

    public class MvxContentPage<TViewModel>
        : MvxContentPage
    , IMvxPage<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
