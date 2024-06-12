import { Routes, Route } from "react-router-dom";

import { Details, Home } from '../pages/index'
const CRoutes = () => {
    return (
        <>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/details" element={<Details />} />
            </Routes>
        </>
    )
}

export default CRoutes;