namespace InterfaceAquisicaoDadosMotorDc.Core.Types
{
    internal struct Either<TLeft, TRight> 
        where TLeft : class
        where TRight : class
    {
        private readonly bool isLeft;

        public bool IsLeft { get => isLeft; }
        public bool IsRight { get => !isLeft; }

        private readonly TRight rightValue;
        private readonly TLeft leftValue;

        private Either(TRight rightValue, TLeft leftValue, bool isLeft)
        {
            this.rightValue = rightValue;
            this.leftValue = leftValue;
            this.isLeft = isLeft;
        }

        public static Either<TLeft, TRight> right(TRight value)
        {
            return new Either<TLeft, TRight>(value, null!, false);
        }

        public static Either<TLeft, TRight> left(TLeft value)
        {
            return new Either<TLeft, TRight>(null!, value, true);
        }

        public TRight unsafeGetRight()
        {
            if (rightValue is null)
            {
                throw new ArgumentNullException(nameof(rightValue));
            }
            
            return this.rightValue;
        }

        public TLeft unsafeGetLeft()
        {
            if (leftValue is null)
            {
                throw new ArgumentNullException(nameof(leftValue));
            }

            return this.leftValue;
        }

        public TReturn match<TReturn>(Func<TRight, TReturn> rightConsumer, Func<TLeft, TReturn> leftConsumer)
        {
            if (isLeft)
            {
                return leftConsumer.Invoke(leftValue);
            }
            else
            {
                return rightConsumer.Invoke(rightValue);
            }
        }

        public void match(Action<TRight> rightConsumer, Action<TLeft> leftConsumer)
        {
            if (isLeft)
            {
                leftConsumer.Invoke(leftValue);
            }
            else
            {
                rightConsumer.Invoke(rightValue);
            }
        }
    }
}
