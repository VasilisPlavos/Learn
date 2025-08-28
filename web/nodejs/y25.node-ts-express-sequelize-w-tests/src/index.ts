import app from "./app";

const PORT = process.env.PORT || 3001;

const initServer = () => {
    app.listen(PORT, () => {
        console.log(`server running. Check http://localhost:${PORT}/api/v1/jokes/2`);
    });
}

initServer();