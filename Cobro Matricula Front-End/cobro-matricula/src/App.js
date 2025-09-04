import "./App.css";

import { AllRoutes } from "./routes/AllRoutes";
import { Footer, Header } from "./components";

function App() {
	return (
		<main className={`App bg-[url('./assets/Fondo.jpg')]`}>
			<Header />
			<AllRoutes />
			<Footer />
		</main>
	);
}

export default App;
