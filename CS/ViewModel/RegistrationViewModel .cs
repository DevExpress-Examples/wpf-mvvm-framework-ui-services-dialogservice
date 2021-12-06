using DevExpress.Mvvm;

namespace Example.ViewModel {
    public class RegistrationViewModel : ViewModelBase {
        public string UserName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
