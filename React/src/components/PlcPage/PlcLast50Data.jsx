import React, { useEffect, useState } from 'react';
import axios from 'axios';
import FilterTableSql from '../Filter/FilterTableSQL';

const PlcLast50Data = ({ page,resetErrors,setResetErrors }) => {
    const [datas, setDatas] = useState([]);
    const [latestDataId, setLatestDataId] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            const res = await axios.post('https://localhost:7189/api/getsqldata', { page: page, last50: true });
            setDatas(res.data);
            if (res.data.length > 0) {
                setLatestDataId(res.data[0].dataId);
            }
        };

        fetchData();
        const interval = setInterval(async () => {
            const res = await axios.post('https://localhost:7189/api/getsqldata', { page: page, last50: true });
            if (res.data.length > 0 && res.data[0].dataId !== latestDataId) {
                setDatas(res.data);
                setResetErrors(true);
                setLatestDataId(res.data[0].dataId);
            }
        }, 5000); 

        return () => clearInterval(interval);
    }, [page, latestDataId]);

    return (
        <>
            {datas.length === 0 ? <div>Veri Bulunamadı</div> : <FilterTableSql data={datas} page={page} />}
        </>
    );
};

export default PlcLast50Data;
