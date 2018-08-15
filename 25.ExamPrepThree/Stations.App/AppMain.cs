namespace Stations.App
{
    using System.Data.Entity;
    using System.IO;

    using Stations.Data;
    using Stations.Export;
    using Stations.Import;

    public class AppMain
    {
        public static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<StationsContext>());
            
            StationsContext context = new StationsContext();
            QueryHelper queryHelper = new QueryHelper(context);
            
            ImportHandler importHandler = new ImportHandler(queryHelper);

            // Import JSON.
            importHandler.ImportStations(File.ReadAllText("../../Datasets/stations.json"));
            importHandler.ImportClasses(File.ReadAllText("../../Datasets/classes.json"));
            importHandler.ImportTrains(File.ReadAllText("../../Datasets/trains.json"));
            importHandler.ImportTrips(File.ReadAllText("../../Datasets/trips.json"));

            // Import XML.
            importHandler.ImportCards(File.ReadAllText("../../Datasets/cards.xml"));
            importHandler.ImportTickets(File.ReadAllText("../../Datasets/tickets.xml"));
            
            ExportHandler exportHandler = new ExportHandler(queryHelper);
            
            // Export JSON.
            exportHandler.ExportDelayedTrains("../../Datasets/", "01/01/2017");
            exportHandler.ExportCardsTicket("../../Datasets/", "Elder");
        }
    }
}
