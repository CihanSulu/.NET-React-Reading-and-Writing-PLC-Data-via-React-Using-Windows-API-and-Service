import React, { useState, useEffect } from 'react';
import FilterTable from '../Filter/FilterTable';
import axios from 'axios';

const PlcNowData = ({ page,resetErrors,setResetErrors }) => {
    const [datas, setDatas] = useState([]);

    useEffect(() => {
        const fetchData = () => {
            axios.post('https://localhost:7189/api/getdatas', { page: page })
                .then(res => {
                    setDatas(res.data);
                })
                .catch(err => {
                    console.error("Veri çekme hatası:", err);
                });
        };

        fetchData();
        const interval = setInterval(fetchData, 5000);
        return () => clearInterval(interval);
    }, [page]);

    return (
        <>
            {datas.length === 0 ? <div>Yükleniyor...</div> : <FilterTable data={datas} page={page} resetErrors={resetErrors} setResetErrors={setResetErrors} />}
        </>
    );
};

export default PlcNowData;
