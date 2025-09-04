import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import "./output.css";
import { Provider } from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import App from "./App";
import { ScrollToTop } from "./components";
import { store } from "./redux/store";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
	<React.StrictMode>
		<Provider store={store}>
			<Router>
				<ScrollToTop />
				<App />
			</Router>
		</Provider>
	</React.StrictMode>,
);
