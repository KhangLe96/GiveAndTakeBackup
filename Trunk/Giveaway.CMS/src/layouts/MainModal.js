import React from 'react';
import { Modal, Spin } from 'antd';
import { connect } from 'dva';
import LoginFormModal from '../components/Modals/Login';

const RenderComponent = ({ component }) => {
  switch (component) {
    case 'login':
      return <LoginFormModal />;
    default:
      return (<div style={{
        display: 'flex',
        alignItems: 'center',
        alignContent: 'center',
        justifyContent: 'center',
        height: '300px',
      }}
      ><Spin />
              </div>);
  }
};

const MainModal = ({ visible, component, dispatch, ...others }) => {
  const handleCancel = () => {
    dispatch({
      type: 'modals/hide',
    });
  };
  return (
    <Modal
      wrapClassName="vertical-center-modal"
      footer={false}
      onCancel={handleCancel}
      visible={visible}
      width="570px"
      closable={component !== 'account-activation'}
      maskClosable={component !== 'account-activation'}
      {...others}
    >
      <Spin spinning={others.loading}>
        {RenderComponent({ component })}
      </Spin>
    </Modal>);
};

export default connect(({ modals }) => ({
  ...modals,
}))(MainModal);
