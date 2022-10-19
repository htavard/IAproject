using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA2022
{
    public class Node2 : GenericNode 
    {
        public int x; //position actuelle de la node
        public int y;

        // Méthodes abstrates, donc à surcharger obligatoirement avec override dans une classe fille
        public override bool IsEqual(GenericNode N2)
        {
            Node2 N2bis = (Node2)N2;

            return (x == N2bis.x) && (y == N2bis.y);
        }

        public override double GetArcCost(GenericNode N2)
        {
            // Ici, N2 ne peut être qu'1 des 8 voisins, inutile de le vérifier
            Node2 N2bis = (Node2)N2;
            double dist = Math.Sqrt((N2bis.x-x)*(N2bis.x-x)+(N2bis.y-y)*(N2bis.y-y));
            if (Form1.matrice[x, y] == -1)
                // On triple le coût car on est dans un marécage
                dist = dist*3;
            return dist;
        }

        public override bool EndState()
        {
            return (x == Form1.xfinal) && (y == Form1.yfinal);
        }

        public override List<GenericNode> GetListSucc()
        {
            List<GenericNode> lsucc = new List<GenericNode>();

            for (int dx=-1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if ((x + dx >= 0) && (x + dx < Form1.nbcolonnes)
                            && (y + dy >= 0) && (y + dy < Form1.nblignes) && ((dx != 0) || (dy != 0)))
                        if (Form1.matrice[x + dx, y + dy] > -2)
                        {
                            Node2 newnode2 = new Node2();
                            newnode2.x = x + dx;
                            newnode2.y = y + dy;
                            lsucc.Add(newnode2);
                        }
                }

            }
            return lsucc;
        }

        public override double CalculeHCost(int envt)
        {
            if(envt == 1) return HCostEvnt1V2();
            else if(envt == 2) return HCostEvnt2();
            else if(envt == 3) return HCostEvnt3();
            return 0;
        }

        private double Norme(int xCurrent, int yCurrent, int xFin, int yFin)
        {
            return Math.Sqrt(Math.Pow(xFin - xCurrent, 2) + Math.Pow(yFin - yCurrent, 2));
        }

        private double plusCourteSansMarec(int xCurrent, int yCurrent, int xFin, int yFin)
        {
            double heuris = 0;
            while (xFin != xCurrent && yFin != yCurrent)
            {
                if (xFin < xCurrent) // x va a gauche, x reduit
                {
                    if (yFin < yCurrent) //y remonte, y reduit
                    {
                        xCurrent -= 1;
                        yCurrent -= 1;
                    }
                    else
                    {
                        xCurrent -= 1;
                        yCurrent += 1;
                    }
                }
                else
                {
                    if (yFin < yCurrent)
                    {
                        xCurrent += 1;
                        yCurrent -= 1;
                    }
                    else
                    {
                        xCurrent += 1;
                        yCurrent += 1;
                    }
                }
                heuris += Math.Sqrt(2);
            }
            if (xCurrent == xFin)
                heuris += Math.Abs(yFin - yCurrent);
            else
                heuris += Math.Abs(xFin - xCurrent);
            return heuris;
        }

        private double plusCourteAvecMarec(int xCurrent, int yCurrent, int xFin, int yFin)
        {
            double heuris = 0;
            while (xFin != xCurrent && yFin != yCurrent)
            {
                if (xFin < xCurrent) // x va a gauche, x reduit
                {
                    if (yFin < yCurrent) //y remonte, y reduit
                    {
                        xCurrent -= 1;
                        yCurrent -= 1;
                    }
                    else
                    {
                        xCurrent -= 1;
                        yCurrent += 1;
                    }
                }
                else
                {
                    if (yFin < yCurrent)
                    {
                        xCurrent += 1;
                        yCurrent -= 1;
                    }
                    else
                    {
                        xCurrent += 1;
                        yCurrent += 1;
                    }
                }
                if (Form1.matrice[yCurrent, xCurrent] == -1)
                    heuris += 3* Math.Sqrt(2);
                else
                    heuris +=Math.Sqrt(2);
                
            }
            while (xCurrent != xFin || yCurrent!=yFin)
            {
                if(xCurrent!=xFin)
                {
                    if (xCurrent < xFin)
                        xCurrent += 1;
                    else
                        xCurrent -= 1;
                }
                else
                {
                    if (yCurrent < yFin)
                        yCurrent += 1;
                    else
                        yCurrent -= 1;
                }
                if (Form1.matrice[yCurrent, xCurrent] == -1)
                    heuris += 3;
                else
                    heuris += 1;
            }
            return heuris;
        }

        private double HCostEvnt1V1()
        {
            double volDoiseau;
            int xfin = Form1.xfinal;
            int yfin = Form1.yfinal;
            volDoiseau = Math.Sqrt(Math.Pow(xfin - x, 2) + Math.Pow(yfin - y, 2)); 
            return volDoiseau;
        }
        private double HCostEvnt1V2()
        {
            
            int xCurrent = x;
            int yCurrent = y;
            int xfin = Form1.xfinal;
            int yfin = Form1.yfinal;
            return plusCourteAvecMarec(xCurrent, yCurrent, xfin, yfin);
            
        }

        private double HCostEvnt2()
        {
            return 0;
        }
        
        private double HCostEvnt3()
        {
            return 0;
        }

        public override string ToString()
        {
            return Convert.ToString(x)+","+ Convert.ToString(y);
        }
    }
}

