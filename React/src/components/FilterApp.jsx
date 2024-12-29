import React, { useState,useEffect } from 'react'
import PlcStatusAndNav from './PlcStatusAndNav'
import FilterElements from './Filter/FilterElements'


const FilterApp = ({plcStatus}) => {

    return (
        <div className='col-12'>
            <div className='card border-0'>
                <div className='card-body'>
                    <PlcStatusAndNav links={[{title:"İstasyonlar",link:"/"},{ title: "Plc Verilerini Filtrele", link: "/filter" }]} />
                    {<FilterElements />}

                </div>
            </div>
        </div>
    )
}

export default FilterApp