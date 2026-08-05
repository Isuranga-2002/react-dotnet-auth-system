import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { register } from "../services/authService";

function Register() {
    const navigate = useNavigate();

    const [fullName, setFullName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        setError("");

        if (!fullName || !email || !password) {
            setError("Please fill in all fields.");
            return;
        }

        setLoading(true);

        try {

            await register({
                fullName,
                email,
                password
            });

            navigate("/", {
                state: {
                    message: "Registration successful. Please log in."
                }
            });

        } catch (err) {

            setError(
                err.response?.data ||
                "Registration failed."
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
                                Create Account
                            </h2>

                            {error && (
                                <div className="alert alert-danger">
                                    {error}
                                </div>
                            )}

                            <form onSubmit={handleSubmit}>

                                <div className="mb-3">
                                    <label className="form-label">
                                        Full Name
                                    </label>

                                    <input
                                        className="form-control"
                                        value={fullName}
                                        onChange={(e) =>
                                            setFullName(e.target.value)
                                        }
                                    />
                                </div>

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
                                    {loading
                                        ? "Registering..."
                                        : "Register"}
                                </button>

                            </form>

                            <div className="text-center mt-3">

                                Already have an account?

                                <Link
                                    to="/"
                                    className="ms-2"
                                >
                                    Login
                                </Link>

                            </div>

                        </div>

                    </div>

                </div>
            </div>
        </div>
    );
}

export default Register;