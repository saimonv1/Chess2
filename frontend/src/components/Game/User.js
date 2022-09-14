import { useRef } from 'react';

const User = (props) => {
    const userNameRef = useRef();

    const onSubmitHandler = async (event) => {
        event.preventDefault();
        //props.onSubmit(userNameRef.current.value);
        try {
            await props.connection.invoke("EnterUserName", userNameRef.current.value);
          } catch (e) {
            console.log(e);
            props.setError(`${e}`);
          }
    };

    return (
        <form onSubmit={onSubmitHandler}>
            <h1>Enter User Name:</h1>
            <input ref={userNameRef} type='text' name='name' />
            <input type="submit" value="Submit" />
            {props.error && <p style={{color: 'red'}}>{props.error}</p>}
        </form>
    );
};

export default User;