using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;

// PayTR'nin form olarak gönderdiği alanlar.
// Geriye PayTR'ye yazılacak cevabı döner (işlendiyse "OK").
public record ProcessPaymentCallbackCommand(string MerchantOid,        // bizim OrderNo
                                            string Status,             // "success" veya "failed"
                                            string TotalAmount,        // kuruş cinsinden, ör. 150,50 TL -> "15050"
                                            string Hash,               // PayTR'nin imzası
                                            string? FailedReasonCode,  // sadece failed'da dolu
                                            string? FailedReasonMsg) : IRequest<string>;