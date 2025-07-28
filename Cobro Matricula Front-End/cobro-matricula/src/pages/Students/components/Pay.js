import { CardPay } from "./CardPay"

export const Pay = ({payment}) => {
  return (
    <div className="w-full grid grid-cols-1 rounded-lg md:grid-cols-2 bg-emerald-950/90 p-4 mb-10">
        {/* {'Matricula Ordinaria'} */}
        <CardPay title={'Ordinaria'} gratuidad={payment.gratuidad} payment={payment} />
        {/* {'Matricula Extraordinaria'} */}
        <CardPay title={'Extraordinaria'} gratuidad={payment.gratuidad} payment={payment} recargoExtraordinaria={payment.RecargoMatriculaExtraordinaria} />
    </div>
  )
}
