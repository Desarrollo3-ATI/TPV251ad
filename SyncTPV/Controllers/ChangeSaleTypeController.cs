using SyncTPV.Models;
using System;
using System.Threading.Tasks;

namespace SyncTPV.Controllers
{
    public class ChangeSaleTypeController
    {
        public static readonly int FROM_CASH_TO_CREDIT = 0;
        public static readonly int FROM_CREDIT_TO_CASH = 1;
        public static int thereAreFormsOfPaymentAdded = 0;
        public static readonly int CREDIT_LIMIT_EXCEEDED = 3;
        public static readonly int DOCUMENT_CONVERTED_TO_CREDIT = 1;
        public static readonly int DOCUMENT_CONVERTED_TO_CASH = 2;


        public async Task<int> doInBackground(int idDocument, int callType)
        {
            int response = 0;
            await Task.Run(async () =>
            {
                if (callType == FROM_CASH_TO_CREDIT)
                {
                    int idCustomer = DocumentModel.getCustomerIdOfADocument(idDocument);
                    double saldoDisponible = CustomerModel.getAvailableBalance(idCustomer);
                    double total = DocumentModel.getTotalForADocumentWithContext(idDocument);
                    if (saldoDisponible == -1 || total <= saldoDisponible)
                    {
                        if (FormasDeCobroDocumentoModel.checkIfThereIsAlreadyASubscriptionToAPaymentMethod(idDocument))
                        {
                            response = thereAreFormsOfPaymentAdded;
                        }
                        else
                        {
                            if (DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument))
                            {
                                if (LicenseModel.isItROMLicense())
                                {
                                    if (DocumentModel.changeDocumentFromCashToCreditOrViceVersa(idDocument, 0, 71, 0))
                                        response = DOCUMENT_CONVERTED_TO_CREDIT;
                                }
                                else if (LicenseModel.isItTPVLicense())
                                {
                                    if (DocumentModel.changeDocumentFromCashToCreditOrViceVersa(idDocument, 2, 71, 0))
                                        response = DOCUMENT_CONVERTED_TO_CREDIT;
                                }
                            }
                            else
                            {
                                response = thereAreFormsOfPaymentAdded;
                            }
                        }
                    }
                    else
                    {
                        /*if (creditLimitExceededAccepted == 1){
                            if (FormasCobroDocumentoModel.checkIfThereIsAlreadyASubscriptionToAPaymentMethod(context, integers[0])){
                                response = thereAreFormsOfPaymentAdded;
                            } else {
                                if (DocumentoVentaModel.verifyThatTheAdvanceIsLessThanTheTotal(context, integers[0])){
                                    if (LicenseModel.isItROMLicense(context)){
                                        if (DocumentoVentaModel.changeDocumentFromCashToCreditOrViceVersa(context, integers[0],
                                                0,71, 0))
                                            response = DOCUMENT_CONVERTED_TO_CREDIT;
                                    } else if (LicenseModel.isItTPVLicense(context)){
                                        if (DocumentoVentaModel.changeDocumentFromCashToCreditOrViceVersa(context, integers[0],
                                                2,71, 0))
                                            response = DOCUMENT_CONVERTED_TO_CREDIT;
                                    }
                                } else {
                                    response = thereAreFormsOfPaymentAdded;
                                }
                            }
                        } else {
                            response = CREDIT_LIMIT_EXCEEDED;
                        }*/
                        response = CREDIT_LIMIT_EXCEEDED;
                    }
                }
                else if (callType == FROM_CREDIT_TO_CASH)
                {
                    Boolean checkFcDocument = FormasDeCobroDocumentoModel.checkIfThereIsAlreadyASubscriptionToAPaymentMethod(idDocument);
                    if (checkFcDocument)
                    {
                        response = thereAreFormsOfPaymentAdded;
                    }
                    else
                    {
                        Boolean advanceValidated = DocumentModel.verifyThatTheAdvanceIsLessThanTheTotal(idDocument);
                        if (advanceValidated)
                        {
                            if (LicenseModel.isItROMLicense())
                            {
                                if (DocumentModel.changeDocumentFromCashToCreditOrViceVersa(idDocument, 0, 0, 0))
                                    response = DOCUMENT_CONVERTED_TO_CASH;
                            }
                            else if (LicenseModel.isItTPVLicense())
                            {
                                if (DocumentModel.changeDocumentFromCashToCreditOrViceVersa(idDocument, 4, 0, 0))
                                    response = DOCUMENT_CONVERTED_TO_CASH;
                            }
                        }
                        else
                        {
                            response = thereAreFormsOfPaymentAdded;
                        }
                    }
                }
                //boolean isChanged = DocumentoVentaModel.updateTypeFromADocument(context, integers[0], integers[2], integers[3]);
            });
            return response;
        }
    }
}
