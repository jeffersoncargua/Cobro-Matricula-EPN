import "./styles/LoadingSquid.css"; //C:\Users\Juan Medina\source\repos\Cobro Matricula EPN\Cobro Matricula Front-End\cobro-matricula\src\components\styles\LoadingSquid.css

export const LoadingSquid = () => {
	return (
		<div
			id="default-modal-squid-loading"
			className="w-full mx-auto flex justify-center z-40 fixed inset-0 bg-gray-100/20"
			tabIndex="-1"
		>
			<div className="flex flex-col md:flex-row items-center justify-center">
				<div className="loader z-50">
					<svg viewBox="0 0 80 80" role="img" aria-label="3D sphere">
						<circle r="32" cy="40" cx="40"></circle>
					</svg>
				</div>

				<div className="loader triangle z-50">
					<svg viewBox="0 0 86 80" role="img" aria-label="3D triangle">
						<polygon points="43 8 79 72 7 72"></polygon>
					</svg>
				</div>

				<div className="loader z-50">
					<svg viewBox="0 0 80 80" role="img" aria-label="3D cube">
						<rect height="64" width="64" y="8" x="8"></rect>
					</svg>
				</div>
			</div>
		</div>
	);
};
