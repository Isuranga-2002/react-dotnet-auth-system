import { login } from "../services/authService";
import { useState } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";

function Login() {

    const navigate = useNavigate();
    const location = useLocation();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const successMessage = location.state?.message;

    const handleSubmit = async (e) => {

        e.preventDefault();

        setError("");

        if (!email || !password) {

            setError("Please fill in all fields.");
            return;

        }

        setLoading(true);

        try {

            const response = await login({
                email,
                password
            });

            localStorage.setItem(
                "token",
                response.data.token
            );

            navigate("/dashboard");

        }
        catch (err) {

            console.log(err);
            console.log(err.response);
            console.log(err.response?.data);

            setError(
                err.response?.data ||
                "Login failed."
            );

        }
        finally {

            setLoading(false);

        }

    };

    return (
        <div className="container mt-5">

            <div className="row justify-content-center">

                <div className="col-md-5">

                    <div className="card shadow">

                        <div className="card-body">

                            <h2 className="text-center mb-4">
                                Login
                            </h2>

                            {successMessage && (
                                <div className="alert alert-success">
                                    {successMessage}
                                </div>
                            )}

                            {error && (
                                <div className="alert alert-danger">
                                    {error}
                                </div>
                            )}

                            <form onSubmit={handleSubmit}>

                                <div className="mb-3">

                                    <label className="form-label">
                                        Email
                                    </label>

                                    <input
                                        type="email"
                                        className="form-control"
                                        value={email}
                                        onChange={(e) =>
                                            setEmail(e.target.value)
                                        }
                                    />

                                </div>

                                <div className="mb-3">

                                    <label className="form-label">
                                        Password
                                    </label>

                                    <input
                                        type="password"
                                        className="form-control"
                                        value={password}
                                        onChange={(e) =>
                                            setPassword(e.target.value)
                                        }
                                    />

                                </div>

                                <button
                                    className="btn btn-primary w-100"
                                    disabled={loading}
                                >

                                    {
                                        loading
                                            ? "Logging in..."
                                            : "Login"
                                    }

                                </button>

                            </form>

                            <div className="text-center mt-3">

                                Don't have an account?

                                <Link
                                    to="/register"
                                    className="ms-2"
                                >
                                    Register
                                </Link>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>
    );

}

export default Login;