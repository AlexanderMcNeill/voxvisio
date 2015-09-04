using System;
using System.Drawing;

namespace VoxVisio
{
    enum eFixationPhase
    {
        start,
        finished
    }

    /// <summary>
    /// Interface to allow basic fixation data collection.
    /// Allows the user to get the fixation's phase (If it is still occuring or has finished), and to get the screen location of the fixation.
    /// </summary>
    interface IFixationData
    {
        void SetFixationTimeStamp(double timesramp);
        Double getFixationTimeStamp();
        eFixationPhase GetFixationPhase();
        Point GetFixationLocation();
        double GetFixationLength();
        void SetFixationLength(double toSet);
        void setFixationFinished();        
    }

    /// <summary>
    /// A class implementing the IFixationData Interface for storing information about the user's fixations.
    /// </summary>
    class Fixation : IFixationData
    {
        
        private readonly Point _location;
        private eFixationPhase _currentEFixationPhase;
        private double _fixationLength;
        private double _timeStamp;

        public Fixation(Point location, eFixationPhase currentEFixationPhase, double timeStamp)
        {
            _location = location;
            _currentEFixationPhase = currentEFixationPhase;
            _timeStamp = timeStamp;
            _fixationLength = 0;
            
        }

        /// <summary>
        /// Set's the fixation to finished.
        /// For use when toggling off a currently running fixation.
        /// </summary>
        public void endFixation()
        {
            _currentEFixationPhase = eFixationPhase.finished;
        }

        public eFixationPhase GetFixationPhase()
        {
            return _currentEFixationPhase;
        }

        /// <summary>
        /// The location of the fixation
        /// </summary>
        /// <returns>A point value for the fixation's location</returns>
        public Point GetFixationLocation()
        {
            return _location;
        }

        public double GetFixationLength()
        {
            return _fixationLength;
        }

        public void setFixationFinished()
        {
            _currentEFixationPhase = eFixationPhase.finished;
        }

        public void SetFixationLength(double toSet)
        {
            _fixationLength = toSet;
        }

        public void SetFixationTimeStamp(double timestamp)
        {
            _timeStamp = timestamp;
        }

        public double getFixationTimeStamp()
        {
            return _timeStamp;
        }
    }
}
