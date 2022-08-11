using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions.Aspect;
using Core.Utilities.Interceptor;
using FluentValidation;

namespace Core.Aspect.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            WrongValidationTypeException.ThrowIfNotEqual(_validatorType, validatorType);

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            IValidator validator = (IValidator)Activator.CreateInstance(_validatorType);
            Type entityType = _validatorType.BaseType.GetGenericArguments()[0];

            IEnumerable<object> entities = invocation.Arguments.Where(x => x.GetType() == entityType);
            foreach (object entity in entities)
                ValidationTool.Validate(validator, entity);
        }
    }
}
