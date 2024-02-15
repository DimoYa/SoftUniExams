﻿using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Repositories
{
    public class FishRepository : IRepository<IFish>
    {
        private List<IFish> _fish;

        public FishRepository()
        {
            this._fish = new List<IFish>();
        }

        public IReadOnlyCollection<IFish> Models => this._fish.AsReadOnly();

        public void AddModel(IFish model)
        {
            this._fish.Add(model);
        }

        public IFish GetModel(string name)
        {
            var currentFish = this.Models.FirstOrDefault(x => x.Name == name);
            return currentFish;
        }
    }
}
