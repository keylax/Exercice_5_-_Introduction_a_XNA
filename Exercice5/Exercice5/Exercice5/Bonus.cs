using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exercice5
{
    public enum BonusTypes {ScoreUp, Life, PowerShoot, MassiveDestruction, HalfPoints};
    class Bonus : Objet2D
    {
        private BonusTypes type { get; set; }

        public Bonus(Texture2D image, Vector2 position, int bonusNumber) : base(image, position)
        {
            switch (bonusNumber)
            {
                case 1:
                    type = BonusTypes.ScoreUp;
                    break;
                case 2:
                    type = BonusTypes.Life;
                    break;
                case 3:
                    type = BonusTypes.PowerShoot;
                    break;
                case 4:
                    type = BonusTypes.MassiveDestruction;
                    break;
                case 5:
                    type = BonusTypes.HalfPoints;
                    break;
            }
              



        }


    }
}
