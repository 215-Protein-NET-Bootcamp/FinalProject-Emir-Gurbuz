using Castle.DynamicProxy;

namespace Core.Utilities.Interceptor
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnAfter(IInvocation invocation) { }

        public override void Intercept(IInvocation invocation)
        {
            bool isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
                var task = invocation.ReturnValue as Task;
                if (task != null)
                    if (task.Exception is not null)
                    {
                        isSuccess = false;
                        OnException(invocation, task.Exception);
                    }
            }
            catch (System.Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
            }
            finally
            {
                if (isSuccess)
                    OnSuccess(invocation);
            }
            OnAfter(invocation);
        }
    }
}
