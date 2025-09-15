import "./App.css";

import { Footer, Header } from "./components";
import { AllRoutes } from "./routes/AllRoutes";

function App() {
	return (
		<main className={`App bg-[url('./assets/Fondo2.jpg')]`}>
			<Header />
			<AllRoutes />
			<Footer />
		</main>
	);
}

export default App;
