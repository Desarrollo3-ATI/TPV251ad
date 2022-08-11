using SyncTPV.Models;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class DeleteWaysToCollectFromADocumentController
    {
        int creditLimitExceededAccepted = 0;

        public DeleteWaysToCollectFromADocumentController(int creditLimitExceededAccepted)
        {
            this.creditLimitExceededAccepted = creditLimitExceededAccepted;
        }

        public async Task<int> doInBackground(int idDocument)
        {
            int response = 0;
            await Task.Run(async () =>
            {
                if (FormasDeCobroDocumentoModel.removePendingBalanceToTheLastFormOfCollectionOfTheDocument(idDocument))
                {
                    if (DocumentModel.updateDocumentAdvance(idDocument, 0))
                    {
                        response = 1;
                    }
                }
            });
            return response;
        }
    }
}
