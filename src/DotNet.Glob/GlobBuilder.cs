﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Globbing.Token;

namespace DotNet.Globbing
{
    public class GlobBuilder : IGlobBuilder
    {
        private readonly List<IGlobToken> _tokens;

        public GlobBuilder()
        {
            _tokens = new List<IGlobToken>();
        }

        public IGlobBuilder AnyCharacter()
        {
            _tokens.Add(new AnyCharacterToken());
            return this;
        }

        public IGlobBuilder OneOf(params char[] characters)
        {
            _tokens.Add(new CharacterListToken(characters, false));
            return this;
        }

        public IGlobBuilder Literal(string text)
        {
            _tokens.Add(new LiteralToken(text));
            return this;
        }

        public IGlobBuilder NotOneOf(params char[] characters)
        {
            _tokens.Add(new CharacterListToken(characters, true));
            return this;
        }

        public IGlobBuilder PathSeperator(PathSeperatorKind kind = PathSeperatorKind.ForwardSlash)
        {
            char seperatorChar;
            switch (kind)
            {
                case PathSeperatorKind.ForwardSlash:
                    seperatorChar = '/';
                    break;
                case PathSeperatorKind.BackwardSlash:
                    seperatorChar = '\\';
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
            _tokens.Add(new PathSeperatorToken(seperatorChar));
            return this;
        }

        public IGlobBuilder Wildcard()
        {
            _tokens.Add(new WildcardToken());
            return this;
        }

        public IGlobBuilder DirectoryWildcard(PathSeperatorKind? leadingSeperatorKind = PathSeperatorKind.ForwardSlash, PathSeperatorKind? trailingSeperatorKind = PathSeperatorKind.ForwardSlash)
        {

            PathSeperatorToken trailingSep = null;
            PathSeperatorToken leadingSep = null;

            if (trailingSeperatorKind == null)
            {
                trailingSep = null;
            }
            else
            {
                switch (trailingSeperatorKind)
                {
                    case PathSeperatorKind.BackwardSlash:
                        trailingSep = new PathSeperatorToken('\\');
                        break;
                    case PathSeperatorKind.ForwardSlash:
                        trailingSep = new PathSeperatorToken('/');
                        break;
                    default:
                        break;
                }
            }

            if (leadingSeperatorKind == null)
            {
                leadingSep = null;
            }
            else
            {
                switch (leadingSeperatorKind)
                {
                    case PathSeperatorKind.BackwardSlash:
                        leadingSep = new PathSeperatorToken('\\');
                        break;
                    case PathSeperatorKind.ForwardSlash:
                        leadingSep = new PathSeperatorToken('/');
                        break;
                    default:
                        break;
                }
            }

            _tokens.Add(new WildcardDirectoryToken(leadingSep, trailingSep));


            return this;
        }

        public IGlobBuilder LetterInRange(char start, char end)
        {
            _tokens.Add(new LetterRangeToken(start, end, false));
            return this;
        }

        public IGlobBuilder LetterNotInRange(char start, char end)
        {
            _tokens.Add(new LetterRangeToken(start, end, true));
            return this;
        }

        public IGlobBuilder NumberInRange(char start, char end)
        {
            _tokens.Add(new NumberRangeToken(start, end, false));
            return this;
        }

        public IGlobBuilder NumberNotInRange(char start, char end)
        {
            _tokens.Add(new NumberRangeToken(start, end, true));
            return this;
        }

        public Glob ToGlob()
        {
            return new Glob(this._tokens.ToArray());
        }

        public List<IGlobToken> Tokens
        {
            get { return _tokens; }
        }


    }
}
