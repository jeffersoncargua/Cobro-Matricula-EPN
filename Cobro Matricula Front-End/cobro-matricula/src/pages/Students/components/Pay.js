import { CardPay } from "./CardPay"

//(showPay && payment !== null)
export const Pay = ({payment,showPay}) => {
  return (
    <div tabIndex={-1} id="payment-section" className={`${(showPay && payment !== null ? '':'hidden')} w-full grid grid-cols-1 rounded-lg md:grid-cols-2 bg-emerald-950/90 p-4 mb-10`}>
        <input  type="text" className="hidden" autoFocus />
        {/* {'Matricula Ordinaria'} */}
        <CardPay title={'Ordinaria'} gratuidad={payment.gratuidad} payment={payment} />
        {/* {'Matricula Extraordinaria'} */}
        <CardPay title={'Extraordinaria'} gratuidad={payment.gratuidad} payment={payment} recargoExtraordinaria={payment.recargoMatriculaExtraordinaria} />
    </div>
  )
}
