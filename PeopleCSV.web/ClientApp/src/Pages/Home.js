import React, { useEffect, useState } from 'react';
import axios from 'axios';
import PersonRow from '../Components/PersonRow';
const Home = () => {

    const [people, setPeople] = useState();
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const getPeople = async () => {
            const { data } = await axios.get('/api/people/getpeople');
            setPeople(data);
            setIsLoading(false);
        }
        getPeople();
    }, [])

    const onDeleteClick = async () => {
        await axios.post('/api/people/deleteall');
        setPeople([]);
    }

    return (
        <>
            <div className="row">
                <div className="col-md-6 offset-md-3 mt-5">
                    <button onClick={onDeleteClick} className="btn btn-danger btn-lg btn-block">Delete All</button>
                </div>
            </div>

            <table className="table table-hover table-striped table-bordered mt-5">
                <thead>
                    <tr>
                        <td>Id</td>
                        <td>First Name</td>
                        <td>Last Name</td>
                        <td>Age</td>
                        <td>Address</td>
                        <td>Email</td>
                    </tr>
                </thead>
                <tbody>
                    {!!people && people.map(p => <PersonRow key={p.id} person={p} />)}

                </tbody>
            </table>
            {isLoading && <h1>Please wait while we work to get your data....</h1>}
        </>)
}
export default Home;