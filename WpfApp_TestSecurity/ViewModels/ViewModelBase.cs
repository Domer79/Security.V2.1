using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoCompare;
using AutoMapper;
using WpfApp_TestSecurity.Annotations;

namespace WpfApp_TestSecurity.ViewModels
{
    public abstract class ViewModelBase<T> : INotifyPropertyChanged where T: class 
    {
        private T OldViewModel { get; set; }
        public virtual event PropertyChangedEventHandler PropertyChanged;
        private T _self;

        protected ViewModelBase()
        {
            _self = this as T;
        }

        public bool IsChanged()
        {
            var comparer = Comparer.Get<T>();
            var diff = comparer(OldViewModel, _self);
            return diff.Count > 0;
        }

        public void Seal()
        {
            OldViewModel = Mapper.Map<T>(_self);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}