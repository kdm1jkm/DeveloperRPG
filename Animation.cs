using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace DeveloperRPG
{
    public class Animation
    {
        private readonly List<KeyValuePair<IntRect, Time>> _rects;
        private int _currentFrame;
        private Time _elapsed;

        public Animation(List<KeyValuePair<IntRect, Time>> rects)
        {
            _rects = rects;
            _currentFrame = 0;
            _elapsed = Time.Zero;
        }

        public void Reset()
        {
            _elapsed = Time.Zero;
            _currentFrame = 0;
        }

        public void Update(Time elapsed)
        {
            _elapsed += elapsed;
            if (_rects[_currentFrame].Value < _elapsed)
            {
                _elapsed = Time.Zero;
                _currentFrame++;
                if (_rects.Count <= _currentFrame) _currentFrame = 0;
            }
        }

        public IntRect GetCurrent()
        {
            return _rects[_currentFrame].Key;
        }
    }
}