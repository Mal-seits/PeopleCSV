import React, { useState } from 'react';

const Generate = () => {
    const [amount, setAmount] = useState(0);

    const onTextChange = e => {
        setAmount(e.target.value);
    }

    const onGenerateClick = () => {
        window.location = (`/api/file/getfile?amount=${amount}`);
    }

    return (
        <div className="d-flex w-100 justify-content-center align-self-center">
            <div className="row">
                <input type="text" onChange={onTextChange} className="form-control-lg" placeholder="Amount" />
            </div>
            <div className="row">
                <div className="col-md-4">
                    <button onClick={onGenerateClick} className="btn btn-primary btn-lg">Generate</button>
                </div>
            </div>
        </div>
    )
}
export default Generate;