import { CardPay } from "./CardPay"

export const Pay = () => {
  return (
    <div className="w-full grid grid-cols-1 rounded-lg md:grid-cols-2 bg-emerald-950/90 p-4 mb-10">
        {/* {'Matricula Ordinaria'} */}
        <CardPay title={'Ordinaria'} gratuidad={'Definitiva'} payment={null} />
        {/* {'Matricula Extraordinaria'} */}
        <CardPay title={'Extraordinaria'} gratuidad={'Definitiva'} payment={null} recargoExtraordinaria={11.45} />
    </div>
  )
}
