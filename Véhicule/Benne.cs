namespace TransConnect.Véhicule
{
    /// <summary>
    /// Ce camion porteur est utilisé pour tous types de travaux publics ou de voirie. 
    /// Suivant son utilisation, les équipements peuvent être très variés. 
    /// Il peut être doté d'une à trois bennes ainsi que d’une grue auxiliaire qui peut être installée sur la cabine du camion. 
    /// Il est couramment utilisé pour transporter du sable, de la terre, du gravier..., 
    /// </summary>
    internal class Benne : Camion, IVehicule
    {
        public int Nb_Bennes { get; }
        public bool Grue { get; }
        /// <summary>
        /// Ce camion porteur est utilisé pour tous types de travaux publics ou de voirie. 
        /// Suivant son utilisation, les équipements peuvent être très variés. 
        /// Il peut être doté d'une à trois bennes ainsi que d’une grue auxiliaire qui peut être installée sur la cabine du camion. 
        /// Il est couramment utilisé pour transporter du sable, de la terre, du gravier..., 
        /// </summary>
        /// <param name="kilometrage">
        /// distance parcouru</param>
        /// <param name="volume">
        /// capactité max que peut contenir le camion</param>
        /// <param name="matiere">
        /// matière qu'il peut transporter</param>
        /// <param name="nb_Bennes">
        /// nombre de benne </param>
        /// <param name="grue">
        /// présence ou non d'une grue auxillaire</param>
        public Benne(int kilometrage, int volume, string matiere, int nb_Bennes, bool grue = false) : base(kilometrage, volume, matiere,1.6)
        {
            Nb_Bennes = nb_Bennes;
            Grue = grue;
        }
        public override string ToString()
        {
            if (Grue)
            {
                return base.ToString() + $"avec {Nb_Bennes} bennes et avec une grue";

            }
            else
            {
                return base.ToString() + $"avec {Nb_Bennes} bennes sans grue";
            }
        }
        public override string ShortString()
        {
            return "un camion Benne";
        }
    }
}
