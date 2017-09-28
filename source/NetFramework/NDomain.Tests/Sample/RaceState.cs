﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDomain.Tests.Sample
{
    public class RaceRunner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
    }

    public class RaceState : NDomain.State
    {
        //<RunnerId, RaceRunner>
        public Dictionary<Guid, RaceRunner> Runners { get; private set; }

        public Guid? WinnerId { get; private set; }

        public int MaxDistance { get; private set; }
        public int MaxRunners { get; private set; }

        public bool Created { get; private set; }
        public bool Started { get; private set; }
        public DateTime StartTimeUtc { get; set; }

        public RaceState()
        {
            this.Runners = new Dictionary<Guid, RaceRunner>();
        }

        public void OnRaceCreated(RaceCreated ev)
        {
            this.MaxDistance = ev.MaxDistance;
            this.MaxRunners = ev.MaxRunners;
            this.Created = true;
        }

        public void OnRaceStarted(RaceStarted ev)
        {
            this.Started = true;
            this.StartTimeUtc = ev.StartTimeUtc;
        }

        public void OnRunnerJoined(RunnerJoined ev)
        {
            var runner = new RaceRunner { Id = ev.RunnerId, Name = ev.RunnerName, Position = 0 };
            this.Runners.Add(runner.Id, runner);
        }

        public void OnRunnerPositionUpdated(RunnerPositionUpdated ev)
        {
            this.Runners[ev.RunnerId].Position = ev.Position;
        }

        public void OnRaceFinished(RaceFinished ev)
        {
            this.WinnerId = ev.WinnerId;
        }
    }
}
