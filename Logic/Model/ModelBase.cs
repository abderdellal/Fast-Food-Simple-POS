using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logic.Model
{
    public class ModelBase : IDataErrorInfo, INotifyPropertyChanged
    {
        public string this[string columnName] => OnValidate(columnName);

        [NotMapped]
        public string Error => null;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            var isValid =
                Validator.TryValidateProperty(GetType().GetProperty(propertyName).GetValue(this), context, results);
            //Validator.TryValidateObject(this, context, results);
            return !isValid ? results[0].ErrorMessage : null;
        }

        public bool IsValid()
        {
            var context = new ValidationContext(this);
            var results = new Collection<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results, true);

            return isValid;
        }
    }
}