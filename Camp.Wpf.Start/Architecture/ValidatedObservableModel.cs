using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Camp.Wpf.Start.Architecture
{
    public class ValidatedObservableModel : ObservableModel,
        INotifyDataErrorInfo
    {
        private readonly IDictionary<string, string[]> _errors =
            new Dictionary<string, string[]>();

        public IEnumerable GetErrors(string propertyName)
        {
            string[] errors;
            _errors.TryGetValue(propertyName, out errors);
            return errors ?? new string[] { };
        }

        public bool HasErrors => _errors.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        protected override void SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            base.SetProperty(ref field, value, propertyName);
            ValidateProperty(value, propertyName);
        }

        private void ValidateProperty<T>(T value, string propertyName)
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(
                value,
                new ValidationContext(this)
                {
                    MemberName = propertyName
                },
                validationResults
            );

            if (validationResults.Any())
            {
                _errors[propertyName] = validationResults
                    .Select(result => result.ErrorMessage)
                    .ToArray();
            }
            else
            {
                _errors.Remove(propertyName);
            }
            ErrorsChanged(
                this,
                new DataErrorsChangedEventArgs(propertyName));
        }
    }
}