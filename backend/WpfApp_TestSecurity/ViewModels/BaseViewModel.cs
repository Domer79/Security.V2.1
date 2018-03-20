using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoCompare;
using AutoMapper;
using WpfApp_TestSecurity.Annotations;

namespace WpfApp_TestSecurity.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        public abstract bool IsChanged();
        public abstract void Seal();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class BaseViewModel<T> : BaseViewModel where T: class 
    {
        private T OldViewModel { get; set; }
        private T _self;

        protected BaseViewModel()
        {
            _self = this as T;
        }

        public sealed override bool IsChanged()
        {
            var comparer = Comparer.Get<T>();
            var diff = comparer(OldViewModel, _self);
            return diff.Count > 0;
        }

        public sealed override void Seal()
        {
            OldViewModel = Mapper.Map<T>(_self);
        }
    }
}