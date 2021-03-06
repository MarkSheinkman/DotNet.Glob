using System.Text;
using DotNet.Globbing.Token;
using System;

namespace DotNet.Globbing.Generation
{
    internal class PathSeperatorMatchGenerator : IMatchGenerator
    {
        private PathSeperatorToken token;
        private Random _random;

        public PathSeperatorMatchGenerator(PathSeperatorToken token, Random _random)
        {
            this.token = token;
            this._random = _random;
        }

        public void AppendMatch(StringBuilder builder)
        {
            builder.Append(token.Value);
        }

        public void AppendNonMatch(StringBuilder builder)
        {
            builder.AppendRandomLiteralCharacter(_random);
        }
    }
}